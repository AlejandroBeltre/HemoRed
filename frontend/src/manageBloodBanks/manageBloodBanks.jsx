import './manageBloodBanks.css';
import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Footer from '../components/footer';
import Headers from '../components/header';
import { ArrowLeftOutlined } from '@ant-design/icons';
import Crisney from '../assets/images/CrisNey.png';
import NewYork from '../assets/images/NewYork.png';
import SanDiego from '../assets/images/SanDiego.png';

const BloodTypeDisplay = ({ type }) => {
    const navigate = useNavigate();
    return (
        <div className='blood-type-display'>
            <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#BF2C32">
                <path d="M480-80q-137 0-228.5-94T160-408q0-100 79.5-217.5T480-880q161 137 240.5 254.5T800-408q0 140-91.5 234T480-80Zm0-80q104 0 172-70.5T720-408q0-73-60.5-165T480-774Q361-665 300.5-573T240-408q0 107 68 177.5T480-160Zm-120-80h240v-80H360v80Zm80-120h80v-80h80v-80h-80v-80h-80v80h-80v80h80v80Zm40-120Z" />
            </svg>
            <span className='blood-type-bank-manage'>{type}</span>
        </div>
    );
}

const BloodBankCard = ({ bank, toggleActiveStatus }) => {
    const navigate = useNavigate();
    const navigateToEditPage = () => {
        navigate(`/manageBloodInventory/modifyBloodInventory/${bank.id}`);
    }
    return (
        <div className="blood-bank-card">
            <div className="card-header">
                <img src={bank.logo} alt={`${bank.name} logo`} className="blood-bank-logo" />
                <h2 className="blood-inventory-bank-name">{bank.name}</h2>
                <span className={`bank-status ${bank.isActive ? 'status-active' : 'status-inactive'}`}>
                    {bank.isActive ? 'Activo' : 'Inactivo'}
                </span>
            </div>
            <div className="blood-inventory-card-content">
                <p className="bank-info">{bank.address}</p>
                <p className="bank-info">{bank.phoneNumber}</p>
                <p className="bank-info">{bank.schedule}</p>
                <div className="blood-types-grid">
                    {bank.bloodTypes.map((bloodType) => (
                        <BloodTypeDisplay key={bloodType.type} type={bloodType.type} />
                    ))}
                </div>
            </div>
            <div className="button-group">
                <button 
                    className={`toggle-active-button ${bank.isActive ? 'deactivate' : 'activate'}`} 
                    onClick={() => toggleActiveStatus(bank.id)}
                >
                    {bank.isActive ? 'Desactivar' : 'Activar'}
                </button>
                <button className="edit-button-manage-blood-banks" onClick={navigateToEditPage}>Editar</button>
            </div>
        </div>
    );
};

function ManageBloodBanks() {
    const [bloodBanks, setBloodBanks] = useState([
        {
            id: 1,
            name: "Blood Bank of Alaska",
            isActive: true,
            address: "1215 Airport Heights Dr, Anchorage, AK 99508, USA",
            phoneNumber: "+1 (907) 222-5630",
            schedule: "7:00AM - 7:00PM",
            logo: Crisney,
            bloodTypes: [
                { type: "A+" },
                { type: "A-" },
                { type: "B+" },
                { type: "B-" },
                { type: "AB+" },
                { type: "AB-" },
                { type: "O+" },
                { type: "O-" },
            ]
        },
        {
            id: 2,
            name: "New York Blood Center",
            isActive: false,
            address: "619 W 54th St, New York, NY 10019",
            phoneNumber: "1-800-000-0000",
            schedule: "9:00AM - 7:00PM",
            logo: NewYork,
            bloodTypes: [
                { type: "A+" },
                { type: "A-" },
                { type: "B+" },
                { type: "B-" },
                { type: "AB+" },
                { type: "AB-" },
                { type: "O+" },
                { type: "O-" },
            ]
        },
        {
            id: 3,
            name: "San Diego Blood Bank",
            isActive: true,
            address: "3636 Gateway Center Ave, San Diego, CA 92102, USA",
            phoneNumber: "+1 (619) 400-8251",
            schedule: "6:30AM - 5:00PM",
            logo: SanDiego,
            bloodTypes: [
                { type: "A+" },
                { type: "A-" },
                { type: "B+" },
                { type: "B-" },
                { type: "AB+" },
                { type: "AB-" },
                { type: "O+" },
                { type: "O-" },
            ]
        }
    ]);

    const [searchTerm, setSearchTerm] = useState('');
    const [filteredBanks, setFilteredBanks] = useState(bloodBanks);

    useEffect(() => {
        // Update filtered banks whenever bloodBanks or searchTerm changes
        const filtered = bloodBanks.filter(bank => 
            bank.name.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredBanks(filtered);
    }, [bloodBanks, searchTerm]);

    
    const toggleActiveStatus = (id) => {
        const updatedBanks = bloodBanks.map(bank => {
            if (bank.id === id) {
                return { ...bank, isActive: !bank.isActive };
            }
            return bank;
        });
        setBloodBanks(updatedBanks);
    };

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
    };

    const navigate = useNavigate();
    const navigateToAddBloodPage = () => {
        navigate('/addBloodBank');
    };
    const handleBack = () => {
        navigate(-1);
    };

    return (
        <div>
            <Headers />
            <ArrowLeftOutlined className='back' onClick={handleBack} />
            <div className="view-blood-request-status-container">
                <h1>Gestión de bancos de sangre</h1>
                <div className="bank-selection">
                    <input 
                        type="text" 
                        value={searchTerm} 
                        onChange={handleSearchChange} 
                        placeholder="Banco de sangre"
                        className='blood-banks-filtering'
                    />
                    {searchTerm && (
                        <div className="search-results">
                        {filteredBanks.map((bank) => (
                        <div 
                            key={bank.id} 
                            onClick={() => navigateToAddBloodPage(bank.id)}
                            className="search-result-item"
                        >
                        </div>
                    ))}
                </div>
            )}
            <button onClick={navigateToAddBloodPage} className="add-blood-btn">Añadir banco</button>
        </div>
        <div className='blood-bank-inventory-grid'>
            {filteredBanks.map((bank) => (
                <BloodBankCard 
                    key={`${bank.id}-${bank.isActive}`} 
                    bank={bank} 
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