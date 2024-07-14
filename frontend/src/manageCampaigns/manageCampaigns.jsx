import './manageCampaigns.css';
import Footer from '../components/footer';
import Headers from '../components/header';
import { ArrowLeftOutlined } from '@ant-design/icons';
import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getCampaigns, getAddressById } from '../api'; // Import getAddressById

const CampaignCard = ({ campaign, address, toggleActiveStatus }) => {
    const navigate = useNavigate();
    const navigateToEditPage = () => {
        navigate(`/manageCampaigns/modifyCampaigns/${campaign.id}`);
    }
    return (
        <div className="blood-bank-card">
            <div className="card-header">
                <h2 className="campaign-name">{campaign.name}</h2>
                <span className={`campaign-status ${campaign.isActive ? 'status-active' : 'status-inactive'}`}>
                    {campaign.isActive ? 'Activa' : 'Inactiva'}
                </span>
            </div>
            <div className="image-campaign-and-bank-name">
                <img src={campaign.image} alt={`${campaign.name} logo`} className="campaign-logo" />
                <p>AFPHumano</p>
            </div>
            <div className="campaigns-card-content">
                <div className="campaign-info">
                    <svg className="inline-icon" xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#666666"><path d="M480-480q33 0 56.5-23.5T560-560q0-33-23.5-56.5T480-640q-33 0-56.5 23.5T400-560q0 33 23.5 56.5T480-480Zm0 294q122-112 181-203.5T720-552q0-109-69.5-178.5T480-800q-101 0-170.5 69.5T240-552q0 71 59 162.5T480-186Zm0 106Q319-217 239.5-334.5T160-552q0-150 96.5-239T480-880q127 0 223.5 89T800-552q0 100-79.5 217.5T480-80Zm0-480Z" /></svg>
                    <p className="bank-info">{address ? `${address.street}, ${address.buildingNumber}` : 'Loading...'}</p>
                </div>
                <div className="campaign-info">
                    <svg className="inline-icon" xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#666666"><path d="M798-120q-125 0-247-54.5T329-329Q229-429 174.5-551T120-798q0-18 12-30t30-12h162q14 0 25 9.5t13 22.5l26 140q2 16-1 27t-11 19l-97 98q20 37 47.5 71.5T387-386q31 31 65 57.5t72 48.5l94-94q9-9 23.5-13.5T670-390l138 28q14 4 23 14.5t9 23.5v162q0 18-12 30t-30 12ZM241-600l66-66-17-94h-89q5 41 14 81t26 79Zm358 358q39 17 79.5 27t81.5 13v-88l-94-19-67 67ZM241-600Zm358 358Z" /></svg>
                    <p className="bank-info">(809) 556-3287</p>
                </div>
                <div className="campaign-info">
                    <svg className="inline-icon" xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#666666"><path d="M200-80q-33 0-56.5-23.5T120-160v-560q0-33 23.5-56.5T200-800h40v-80h80v80h320v-80h80v80h40q33 0 56.5 23.5T840-720v560q0 33-23.5 56.5T760-80H200Zm0-80h560v-400H200v400Zm0-480h560v-80H200v80Zm0 0v-80 80Z" /></svg>
                    <p className="bank-info">
                        {new Date(campaign.startTimestamp).toLocaleString('es-US', {
                            month: 'long',
                            day: 'numeric',
                            year: 'numeric',
                            hour: 'numeric',
                            minute: 'numeric',
                            hour12: true
                        })} - {new Date(campaign.endTimestamp).toLocaleString('es-US', {
                            month: 'long',
                            day: 'numeric',
                            year: 'numeric',
                            hour: 'numeric',
                            minute: 'numeric',
                            hour12: true
                        })}
                    </p>
                </div>
            </div>
            <div className="button-group">
                <button
                    className={`toggle-active-button ${campaign.isActive ? 'deactivate' : 'activate'}`}
                    onClick={() => toggleActiveStatus(campaign.id)}
                >
                    {campaign.isActive ? 'Desactivar' : 'Activar'}
                </button>
                <button className="edit-button-manage-blood-banks" onClick={navigateToEditPage}>Editar</button>
            </div>
        </div>
    );
};

function ManageCampaigns() {
    const [campaigns, setCampaigns] = useState([]);
    const [addresses, setAddresses] = useState({});
    const [searchTerm, setSearchTerm] = useState('');
    const [filteredCampaigns, setFilteredCampaigns] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchCampaigns = async () => {
            try {
                const response = await getCampaigns();
                setCampaigns(response);
                setFilteredCampaigns(response);

                // Fetch addresses
                const addressPromises = response.map(campaign => getAddressById(campaign.addressID));
                const addressResponses = await Promise.all(addressPromises);
                const addressData = addressResponses.reduce((acc, addressResponse) => {
                    acc[addressResponse.data.addressID] = addressResponse.data;
                    return acc;
                }, {});
                setAddresses(addressData);
            } catch (error) {
                console.error('Error fetching campaigns:', error);
            }
        };

        fetchCampaigns();
    }, []);

    useEffect(() => {
        const filtered = campaigns.filter(campaign =>
            campaign.name && campaign.name.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredCampaigns(filtered);
    }, [campaigns, searchTerm]);

    const toggleActiveStatus = (id) => {
        const updatedCampaigns = campaigns.map(campaign => {
            if (campaign.id === id) {
                return { ...campaign, isActive: !campaign.isActive };
            }
            return campaign;
        });
        setCampaigns(updatedCampaigns);
        setFilteredCampaigns(updatedCampaigns.filter(campaign =>
            campaign.name && campaign.name.toLowerCase().includes(searchTerm.toLowerCase())
        ));
    };

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
    };

    const navigateToAddCampaignPage = (id) => {
        navigate(`/manageCampaigns/addCampaigns/${id}`);
    };

    const handleBack = () => {
        navigate(-1);
    };

    return (
        <div>
            <Headers />
            <ArrowLeftOutlined className='back' onClick={handleBack} />
            <div className="view-blood-request-status-container">
                <h1 className='inventory-title'>Gestión de campañas</h1>
                <div className="bank-selection">
                    <input
                        type="text"
                        value={searchTerm}
                        onChange={handleSearchChange}
                        placeholder="Buscar campaña"
                        className='blood-banks-filtering'
                    />
                    {searchTerm && (
                        <div className="search-results">
                            {filteredCampaigns.map((campaign) => (
                                <div
                                    key={campaign.id}
                                    onClick={() => navigateToAddCampaignPage(campaign.id)}
                                    className="search-result-item"
                                >
                                </div>
                            ))}
                        </div>
                    )}
                    <button onClick={() => navigateToAddCampaignPage()} className="add-blood-btn">Crear campaña</button>
                </div>
                <div className='blood-bank-inventory-grid'>
                    {filteredCampaigns.map((campaign) => (
                        <CampaignCard
                            key={`${campaign.id}-${campaign.isActive}`}
                            campaign={campaign}
                            address={addresses[campaign.addressID]}
                            toggleActiveStatus={toggleActiveStatus}
                        />
                    ))}
                </div>
            </div>
            <Footer />
        </div>
    );
}

export default ManageCampaigns;