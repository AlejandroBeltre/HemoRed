import React from 'react';
import { useNavigate } from 'react-router-dom';
import './dashboardUser.css';
import { useState, useEffect } from 'react';
import Headers from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined } from '@ant-design/icons';
import Card from '../components/card';

function DashboardAdmin() {
    const navigate = useNavigate();
    const handleBack = () => {
        navigate(-1);
    };

    return (
        <div>
            <Headers />
            <ArrowLeftOutlined onClick={handleBack} className="back" />
            <div className="dashboard-user-container">
                <h1>Dashboard de admin</h1>
                <div className="cards-container">
                    <Card title="Tu perfil" linkText="Editar perfil" linkUrl="/modifyProfile">
                        <p>Nombre</p>
                        <p>Teléfono</p>
                        <p>Dirección</p>
                    </Card>
                    <Card title="Top bancos">
                        <p>Banco de sangre: 2024-06-01</p>
                        <p>Banco de sangre: 2024-06-01</p>
                        <p>Banco de sangre: 2024-06-01</p>
                        <p>Banco de sangre: 2024-06-01</p>
                    </Card>
                    <Card title="Campañas" linkText="Ver campañas" linkUrl="/manageCampaigns">
                        <p>Campaña</p>
                        <p>Campaña</p>
                        <p>Campaña</p>
                        <p>Campaña</p>
                    </Card>
                </div>
            </div>
            <Footer />
        </div>
    );
};

export default DashboardAdmin;