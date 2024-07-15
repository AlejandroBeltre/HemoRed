import React, { useState, useEffect } from 'react';
import './manageBloodInventory.css';
import Headers from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { getBloodBags, getBloodBanks, getAddressById, getBloodTypes } from '../api';

const BloodTypeDisplay = ({ type, bags }) => {
    return (
        <div className='blood-type-display'>
            <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#BF2C32">
                <path d="M480-80q-137 0-228.5-94T160-408q0-100 79.5-217.5T480-880q161 137 240.5 254.5T800-408q0 140-91.5 234T480-80Zm0-80q104 0 172-70.5T720-408q0-73-60.5-165T480-774Q361-665 300.5-573T240-408q0 107 68 177.5T480-160Zm-120-80h240v-80H360v80Zm80-120h80v-80h80v-80h-80v-80h-80v80h-80v80h80v80Zm40-120Z" />
            </svg>
            <span className='blood-type-bank-manage'>{type}:</span>
            <span className='blood-bags'>{bags}</span>
        </div>
    );
};

const BloodBankCard = ({ bank, address, bloodBags, bloodTypeMap }) => {
    const navigate = useNavigate();
    const navigateToEditPage = () => {
        navigate(`/manageBloodInventory/modifyBloodInventory/${bank.bloodBankID}`);
    };

    const bloodTypes = bloodBags.reduce((acc, bag) => {
        const type = bloodTypeMap[bag.bloodTypeID];
        if (!acc[type]) {
            acc[type] = 0;
        }
        acc[type] += 1; // Increment the count for each bag with the same BloodTypeID
        return acc;
    }, {});

    return (
        <div className="blood-bank-card">
            <div className="card-header">
                <h2 className="blood-inventory-bank-name">{bank.bloodBankName}</h2>
                <span className={`bank-status ${bank.isActive ? 'status-inactive' : 'status-active'}`}>
                    {bank.isActive ? 'Inactivo' : 'Activo'}
                </span>
            </div>
            <div className="blood-inventory-card-content">
                <p className="bank-info">Dirección: {address ? `${address.street}, ${address.buildingNumber}` : 'Loading...'}</p>
                <p className="bank-info">Teléfono: {bank.phone}</p>
                <div className="blood-types-grid">
                    {Object.entries(bloodTypes).map(([type, bags]) => (
                        <BloodTypeDisplay key={type} type={type} bags={bags} />
                    ))}
                </div>
            </div>
            <button className="edit-button" onClick={navigateToEditPage}>Editar</button>
        </div>
    );
};

function ManageBloodInventory() {
    const [bloodBanks, setBloodBanks] = useState([]);
    const [addresses, setAddresses] = useState({});
    const [bloodBags, setBloodBags] = useState([]);
    const [bloodTypes, setBloodTypes] = useState({});
    const [searchTerm, setSearchTerm] = useState('');
    const [filteredBanks, setFilteredBanks] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchBloodInventory = async () => {
            try {
                const bloodBagsResponse = await getBloodBags();
                setBloodBags(bloodBagsResponse.data);

                const bloodBanksResponse = await getBloodBanks();
                setBloodBanks(bloodBanksResponse.data);
                setFilteredBanks(bloodBanksResponse.data);

                const addressPromises = bloodBanksResponse.data.map(bank => getAddressById(bank.addressID));
                const addressResponses = await Promise.all(addressPromises);
                const addressData = addressResponses.reduce((acc, addressResponse) => {
                    acc[addressResponse.data.addressID] = addressResponse.data;
                    return acc;
                }, {});
                setAddresses(addressData);

                const bloodTypesResponse = await getBloodTypes();
                const bloodTypeMap = bloodTypesResponse.data.reduce((acc, type) => {
                    acc[type.bloodTypeID] = type.bloodType;
                    return acc;
                }, {});
                setBloodTypes(bloodTypeMap);
            } catch (error) {
                console.error('Error fetching blood inventory:', error);
            }
        };

        fetchBloodInventory();
    }, []);

    useEffect(() => {
        const filtered = bloodBanks.filter(bank =>
            bank.bloodBankName.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredBanks(filtered);
    }, [bloodBanks, searchTerm]);

    useEffect(() => {
        const updatedInventory = JSON.parse(localStorage.getItem('updatedInventory'));
        if (updatedInventory) {
            setBloodBags(prevBags => {
                const updatedBags = [...prevBags];
                const index = updatedBags.findIndex(bag => 
                    bag.bloodBankID === updatedInventory.selectedBank && 
                    bag.bloodTypeID === updatedInventory.bloodType
                );
                if (index !== -1) {
                    updatedBags[index].bags += updatedInventory.quantity;
                } else {
                    updatedBags.push({
                        bloodBankID: updatedInventory.selectedBank,
                        bloodTypeID: updatedInventory.bloodType,
                        bags: updatedInventory.quantity
                    });
                }
                return updatedBags;
            });
        }
    }, []);

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
    };

    const handleBack = () => {
        navigate(-1);
    };

    const navigateToAddBloodPage = () => {
        navigate('/manageBloodInventory/addBloodToInventory');
    };

    return (
        <div>
            <Headers />
            <ArrowLeftOutlined className='back' onClick={handleBack} />
            <div className='view-blood-request-status-container'>
                <h1 className='inventory-title'>Inventario de sangre</h1>
                <div className="bank-selection">
                    <input
                        type="text"
                        value={searchTerm}
                        onChange={handleSearchChange}
                        placeholder="Buscar banco de sangre"
                        className='blood-banks-filtering'
                    />
                    <button onClick={navigateToAddBloodPage} className="add-blood-btn">Añadir sangre a inventarios</button>
                </div>
                <div className='blood-bank-inventory-grid'>
                    {filteredBanks.map((bank) => (
                        <BloodBankCard
                            key={bank.bloodBankID}
                            bank={bank}
                            address={addresses[bank.addressID]}
                            bloodBags={bloodBags.filter(bag => bag.bloodBankID === bank.bloodBankID)}
                            bloodTypeMap={bloodTypes}
                        />
                    ))}
                </div>
            </div>
            <Footer />
        </div>
    );
}

export default ManageBloodInventory;