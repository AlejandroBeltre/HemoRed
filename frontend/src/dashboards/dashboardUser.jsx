import React from 'react';
import { useNavigate } from 'react-router-dom';
import './dashboardUser.css';
import { useState, useEffect } from 'react';
import Headers from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined } from '@ant-design/icons';
import Card from '../components/card';

function DashboardUser() {
    const navigate = useNavigate();
    const handleBack = () => {
        navigate(-1);
    };

    return (
        <div>
            <Headers />
            <ArrowLeftOutlined onClick={handleBack} className="back" />
            <div className="dashboard-user-container">
                <h1>Dashboard de usuario</h1>
                <div className="cards-container">
                    <Card title="Tu perfil" linkText="Editar perfil" linkUrl="/modifyProfile">
                        <p>Tipo de Sangre: O+</p>
                        <p>Ultima Fecha de Donación: 2024-06-01</p>
                    </Card>
                    <Card title="Donaciones" linkText="Historial de donaciones" linkUrl="/viewDonationHistory">
                        <p>Banco de sangre: 2024-06-01</p>
                        <p>Banco de sangre: 2024-06-01</p>
                        <p>Banco de sangre: 2024-06-01</p>
                        <p>Banco de sangre: 2024-06-01</p>
                    </Card>
                    <Card title="Adquisiciones">
                        <p>Banco de sangre: 2024-06-01</p>
                        <p>Banco de sangre: 2024-06-01</p>
                    </Card>
                </div>
            </div>
            <Footer />
        </div>
    );
};

export default DashboardUser;