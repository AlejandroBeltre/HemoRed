import React, { useState } from 'react';
import './campaigns.css';
import Header from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import campañasActivas from '../assets/images/campañasActivas.png';
import diaDelDonador from '../assets/images/diaDelDonador.png';
import regalaVida from '../assets/images/regalaVida.png';
import sangreParaTodos from '../assets/images/sangreParaTodos.png';

const CampaignCard = ({ campaign }) => {
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
            <img src={campaign.logo} alt={`${campaign.name} logo`} className="campaign-logo" />
            <p>{campaign.bankName}</p>
        </div>
        <div className="campaigns-card-content">
            <div className="campaign-info">
                <svg className="inline-icon" xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#666666"><path d="M480-480q33 0 56.5-23.5T560-560q0-33-23.5-56.5T480-640q-33 0-56.5 23.5T400-560q0 33 23.5 56.5T480-480Zm0 294q122-112 181-203.5T720-552q0-109-69.5-178.5T480-800q-101 0-170.5 69.5T240-552q0 71 59 162.5T480-186Zm0 106Q319-217 239.5-334.5T160-552q0-150 96.5-239T480-880q127 0 223.5 89T800-552q0 100-79.5 217.5T480-80Zm0-480Z" /></svg>
                <p className="bank-info">{campaign.address}</p>
            </div>
            <div className="campaign-info">
                <svg className="inline-icon" xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#666666"><path d="M798-120q-125 0-247-54.5T329-329Q229-429 174.5-551T120-798q0-18 12-30t30-12h162q14 0 25 9.5t13 22.5l26 140q2 16-1 27t-11 19l-97 98q20 37 47.5 71.5T387-386q31 31 65 57.5t72 48.5l94-94q9-9 23.5-13.5T670-390l138 28q14 4 23 14.5t9 23.5v162q0 18-12 30t-30 12ZM241-600l66-66-17-94h-89q5 41 14 81t26 79Zm358 358q39 17 79.5 27t81.5 13v-88l-94-19-67 67ZM241-600Zm358 358Z" /></svg>
                <p className="bank-info">{campaign.phoneNumber}</p>
            </div>
            <div className="campaign-info">
                <svg className="inline-icon" xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#666666"><path d="M200-80q-33 0-56.5-23.5T120-160v-560q0-33 23.5-56.5T200-800h40v-80h80v80h320v-80h80v80h40q33 0 56.5 23.5T840-720v560q0 33-23.5 56.5T760-80H200Zm0-80h560v-400H200v400Zm0-480h560v-80H200v80Zm0 0v-80 80Z" /></svg>
                <p className="bank-info">{campaign.schedule}</p>
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
    const handleBack = () => {
        navigate(-1);
    };

    const campaigns = [
        {
            id: 1,
            name: "¡Sangre para todos!",
            bankName: "Cruz Roja Americana",
            isActive: true,
            address: "123 Elm Street, Springfield, IL 62704, USA",
            phoneNumber: "+1 (555) 123-4567",
            schedule: "13 de junio de 2023 - 7:00 am | \n15 de junio de 2023 - 3:00 pm",
            logo: sangreParaTodos
        },
        {
            id: 2,
            name: "Regala vida",
            bankName: "Servicios de Sangre de Canadá",
            isActive: true,
            address: "456 Maple Avenue, Toronto, ON M4C 1B5, Canada",
            phoneNumber: "+1 293-7400",
            schedule: "14 de junio de 2024 - 9:00 am | \n15 de junio de 2024 - 5:00 pm",
            logo: regalaVida
        },
        {
            id: 3,
            name: "Dia del donador",
            bankName: "Servicios Nacional de Salud de la Sangre y Transplantes",
            isActive: true,
            address: "789 Pine Road, London, SW1A 1AA, United Kingdom",
            phoneNumber: "+44 20 7946 0958",
            schedule: "12 de enero de 2024 - 9:00 am | \n12 de enero de 2024 - 2:00 pm",
            logo: diaDelDonador
        }
    ];

    const navigateToDonatePage = () => {
        navigate('/campaigns/participateCampaign');
    };

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
                        <CampaignCard key={campaign.id} campaign={campaign} />
                    ))}
                </div>
            </div>
            <div className="clearfix"></div>
            <Footer />
        </div>
    );
};

export default Campaigns;