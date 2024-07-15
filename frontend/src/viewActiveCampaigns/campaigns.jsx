import React, { useState, useEffect } from 'react';
import './campaigns.css';
import Header from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { getCampaigns, getAddressById } from '../api'; // Import getAddressById as well
import campañasActivas from '../assets/images/campañasActivas.png';

const CampaignCard = ({ campaign, address }) => {
    const navigate = useNavigate();
    const navigateToParticipateSpecificCampaignPage = () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
        navigate(`/campaigns/participateSpecificCampaign/${campaign.id}`,  { state: { campaign } });
    }

    return (
        <div className="blood-bank-card">
            <div className="card-header">
                <h2 className="campaign-name">{campaign.name}</h2>  
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
                <button className="anotarse-campaña-button" onClick={navigateToParticipateSpecificCampaignPage}>Anotarse en campaña</button>
            </div>
        </div>
    );
};

function Campaigns() {
    const navigate = useNavigate();
    const [campaigns, setCampaigns] = useState([]);
    const [addresses, setAddresses] = useState({});
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchCampaigns = async () => {
            try {
                const response = await getCampaigns();
                setCampaigns(response);

                // Fetch addresses
                const addressPromises = response.map(campaign => getAddressById(campaign.addressID));
                const addressResponses = await Promise.all(addressPromises);
                const addressData = addressResponses.reduce((acc, addressResponse) => {
                    acc[addressResponse.data.addressID] = addressResponse.data;
                    return acc;
                }, {});
                setAddresses(addressData);

                setIsLoading(false);
            } catch (error) {
                console.error('Failed to fetch campaigns:', error);
                setError('Failed to fetch campaigns: ' + error.message);
                setIsLoading(false);
            }
        };

        fetchCampaigns();
    }, []);

    const handleBack = () => {
        navigate(-1);
    };

    const navigateToDonatePage = () => {
        navigate('/campaigns/participateCampaign');
    };

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <Header />
            <ArrowLeftOutlined className="back" onClick={handleBack} />
            <div className="active-campaigns-container">
                <h1>Donar sangre, salvar vidas</h1>
                <div className="content-wrapper">
                    <img src={campañasActivas} alt="Campañas activas" className="left-aligned-img" />
                    <div className="text-button-container">
                        <p>Explora nuestras campañas de donacion activas y haz la diferencia en tu comunidad.</p>
                        <button className="donate-button" onClick={navigateToDonatePage}>Donar Ahora</button>
                    </div>
                </div>
                <h1>Campañas activas</h1>
                <div className="blood-bank-inventory-grid">
                    {campaigns.map((campaign) => (
                        <CampaignCard 
                            key={campaign.id} 
                            campaign={campaign} 
                            address={addresses[campaign.addressID]}
                        />
                    ))}
                </div>
            </div>
            <div className="clearfix"></div>
            <Footer />
        </div>
    );
};

export default Campaigns;