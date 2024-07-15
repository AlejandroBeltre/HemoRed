import './manageBloodBanks.css';
import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Footer from '../components/footer';
import Headers from '../components/header';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { getBloodBanks, getAddressById } from '../api'; // Make sure to import getAddressById

const BloodTypeDisplay = ({ type }) => {
    return (
        <div className='blood-type-display'>
            <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#BF2C32">
                <path d="M480-80q-137 0-228.5-94T160-408q0-100 79.5-217.5T480-880q161 137 240.5 254.5T800-408q0 140-91.5 234T480-80Zm0-80q104 0 172-70.5T720-408q0-73-60.5-165T480-774Q361-665 300.5-573T240-408q0 107 68 177.5T480-160Zm-120-80h240v-80H360v80Zm80-120h80v-80h80v-80h-80v-80h-80v80h-80v80h80v80Zm40-120Z" />
            </svg>
            <span className='blood-type-bank-manage'>{type}</span>
        </div>
    );
};

const BloodBankCard = ({ bank, address, toggleActiveStatus }) => {
    const navigate = useNavigate();
    const navigateToEditPage = () => {
        navigate(`/manageBloodBanks/modifyBloodBank/${bank.bloodBankID}`);
    };
    return (
        <div className="blood-bank-card">
            <div className="card-header">
                {bank.image ? (
                    <img src={bank.image} alt={`${bank.bloodBankName} logo`} className="blood-bank-logo" />
                ) : (
                    <div className="no-image">Imagen no disponible</div>
                )}
            </div>
            <div className="card-content">
                <h2 className="blood-inventory-bank-name">{bank.bloodBankName}</h2>
                <p className="bank-info">Dirección: {address ? `${address.street}, ${address.buildingNumber}` : 'Loading...'}</p>
                <p className="bank-info">Teléfono: {bank.phone}</p>
                <p className="bank-info">Horas Disponibles: {bank.availableHours}</p>
            </div>
            <div className="button-group">
                <button className="edit-button-manage-blood-banks" onClick={navigateToEditPage}>Editar</button>
            </div>
        </div>
    );
};

function ManageBloodBanks() {
    const [bloodBanks, setBloodBanks] = useState([]);
    const [addresses, setAddresses] = useState({});
    const [searchTerm, setSearchTerm] = useState('');
    const [filteredBanks, setFilteredBanks] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchBloodBanks = async () => {
            try {
                const response = await getBloodBanks();
                setBloodBanks(response.data);
                setFilteredBanks(response.data);

                // Fetch addresses
                const addressPromises = response.data.map(bank => getAddressById(bank.addressID));
                const addressResponses = await Promise.all(addressPromises);
                const addressData = addressResponses.reduce((acc, addressResponse) => {
                    acc[addressResponse.data.addressID] = addressResponse.data;
                    return acc;
                }, {});
                setAddresses(addressData);
            } catch (error) {
                console.error('Error fetching blood banks:', error);
            }
        };

        fetchBloodBanks();
    }, []);

    useEffect(() => {
        const filtered = bloodBanks.filter(bank => 
            bank.bloodBankName.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredBanks(filtered);
    }, [bloodBanks, searchTerm]);

    const toggleActiveStatus = (id) => {
        const updatedBanks = bloodBanks.map(bank => {
            if (bank.bloodBankID === id) {
                return { ...bank, isActive: !bank.isActive };
            }
            return bank;
        });
        setBloodBanks(updatedBanks);
        setFilteredBanks(updatedBanks.filter(bank => bank.bloodBankName.toLowerCase().includes(searchTerm.toLowerCase())));
    };

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
    };

    const navigateToAddBloodPage = () => {
        navigate('/manageBloodBanks/addBloodBank');
    };

    const handleBack = () => {
        navigate(-1);
    };

    return (
        <div>
            <Headers />
            <ArrowLeftOutlined className='back' onClick={handleBack} />
            <div className="view-blood-request-status-container">
                <h1 className='inventory-title'>Gestión de bancos de sangre</h1>
                <div className="bank-selection">
                    <input 
                        type="text" 
                        value={searchTerm} 
                        onChange={handleSearchChange} 
                        placeholder="Banco de sangre"
                        className='blood-banks-filtering'
                    />
                    <button onClick={navigateToAddBloodPage} className="add-blood-btn">Añadir banco</button>
                </div>
                <div className='blood-bank-inventory-grid'>
                    {filteredBanks.map((bank) => (
                        <BloodBankCard 
                            key={`${bank.bloodBankID}-${bank.isActive}`} 
                            bank={bank} 
                            address={addresses[bank.addressID]}
                            toggleActiveStatus={toggleActiveStatus} 
                        />
                    ))}
                </div>
            </div>
            <Footer />
        </div>
    );
}

export default ManageBloodBanks;