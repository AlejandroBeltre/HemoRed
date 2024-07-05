import React, { useState } from 'react';
import './viewBloodRequestStatus.css';
import Headers from '../components/header.jsx';
import Footer from '../components/footer.jsx';
import { ArrowLeftOutlined, InboxOutlined, ShoppingCartOutlined, CalendarOutlined, ClockCircleOutlined } from '@ant-design/icons';
import { Link, useNavigate } from 'react-router-dom';

function ViewBloodRequestStatus() {
    const bloodRequests = [
        {
            bankId: 1,
            id: 1,
            bankName: "Centro de la sangre y especialidades",
            orderNumber: 123456,
            bloodType: "A+",
            quantity: 2,
            orderState: "En camino",
            dateOfRetiring: "2021-10-15",
            hourOfRetiring: "10:00AM"
        },
        {
            bankId: 2,
            id: 2,
            bankName: "Banco de sangre Fundación Crisney",
            orderNumber: 123456,
            bloodType: "A+",
            quantity: 2,
            orderState: "En camino",
            dateOfRetiring: "2021-10-15",
            hourOfRetiring: "10:00AM"
        },
        {
            bankId: 3,
            id: 3,
            bankName: "Banco de sangre Fundación Crisney",
            orderNumber: 123456,
            bloodType: "A+",
            quantity: 2,
            orderState: "En camino",
            dateOfRetiring: "2021-10-15",
            hourOfRetiring: "10:00AM"
        },
        {
            bankId: 3,
            id: 3,
            bankName: "Banco de sangre Fundación Crisney",
            orderNumber: 123456,
            bloodType: "A+",
            quantity: 2,
            orderState: "En camino",
            dateOfRetiring: "2021-10-15",
            hourOfRetiring: "10:00AM"
        },
        {
            bankId: 3,
            id: 3,
            bankName: "Banco de sangre Fundación Crisney",
            orderNumber: 123456,
            bloodType: "A+",
            quantity: 2,
            orderState: "En camino",
            dateOfRetiring: "2021-10-15",
            hourOfRetiring: "10:00AM"
        },
        {
            bankId: 3,
            id: 3,
            bankName: "Banco de sangre Fundación Crisney",
            orderNumber: 123456,
            bloodType: "A+",
            quantity: 2,
            orderState: "En camino",
            dateOfRetiring: "2021-10-15",
            hourOfRetiring: "10:00AM"
        },
    ];
    const navigate = useNavigate();
    const handleBack = () => {
        navigate(-1);
    }
    return (
        <div>
            <Headers />
            <ArrowLeftOutlined className="back" onClick={handleBack} />
            <div className='view-blood-request-status-container'>
                <h1>Estados de adquisición</h1>
                <div className='blood-request-status-list'>
                    {bloodRequests.map((bloodRequest) => (
                        <div key={bloodRequest.id} className='blood-request-status-block'>
                            <div className='blood-request-status-header'>
                                <h2>{bloodRequest.bankName}</h2>
                            </div>
                            <div className='blood-request-status-body'>
                                <div className='blood-request-status-item'>
                                    <InboxOutlined style={{ fontSize: '24px', color: '#BF2C32' }} />
                                    <p>
                                        <span className="label">Número de orden:</span>
                                        <span className="value">{bloodRequest.orderNumber}</span>
                                    </p>
                                </div>
                                <div className='blood-request-status-item'>
                                    <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#BF2C32">
                                        <path d="M480-80q-137 0-228.5-94T160-408q0-100 79.5-217.5T480-880q161 137 240.5 254.5T800-408q0 140-91.5 234T480-80Zm0-80q104 0 172-70.5T720-408q0-73-60.5-165T480-774Q361-665 300.5-573T240-408q0 107 68 177.5T480-160Zm-120-80h240v-80H360v80Zm80-120h80v-80h80v-80h-80v-80h-80v80h-80v80h80v80Zm40-120Z" />
                                    </svg>
                                    <p>
                                        <span className="label">Tipo de sangre:</span>
                                        <span className="value">{bloodRequest.bloodType}</span>
                                    </p>
                                </div>
                                <div className='blood-request-status-item'>
                                    <ShoppingCartOutlined style={{ fontSize: '24px', color: '#BF2C32' }} />
                                    <p>
                                        <span className="label">Cantidad:</span>
                                        <span className="value">{bloodRequest.quantity}</span>
                                    </p>
                                </div>
                                <div className='blood-request-status-item'>
                                    <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#BF2C32"><path d="M315-240q-77 0-117-57t-38-128l-18-27q-11-17-36.5-77T80-680q0-103 51-171.5T260-920q85 0 132.5 75.5T440-680q0 58-16 107t-28 79l8 13q8 14 22 44.5t14 63.5q0 57-35.5 95T315-240ZM210-496l110-22q13-32 26.5-73t13.5-89q0-60-27.5-110T260-840q-45 0-72.5 50T160-680q0 63 17.5 111.5T210-496Zm105 176q19 0 32-14t13-39q0-17-8-35t-16-32l-96 20q0 40 17.5 70t57.5 30ZM645-40q-54 0-89.5-38T520-173q0-33 14-63.5t22-44.5l8-13q-12-30-28-79t-16-107q0-89 47.5-164.5T700-720q78 0 129 68.5T880-480q0 91-25.5 150.5T818-253l-18 28q1 71-38.5 128T645-40Zm105-256q15-24 32.5-72T800-480q0-60-27.5-110T700-640q-45 0-72.5 50T600-480q0 48 13.5 88.5T640-318l110 22ZM645-120q40 0 57.5-30t17.5-70l-96-20q-8 14-16 32t-8 35q0 20 12.5 36.5T645-120Z" /></svg>
                                    <p>
                                        <span className="label">Estado de la orden:</span>
                                        <span className="value">{bloodRequest.orderState}</span>
                                    </p>
                                </div>
                                <div className='blood-request-status-item'>
                                    <CalendarOutlined style={{ fontSize: '24px', color: '#BF2C32' }} />
                                    <p>
                                        <span className="label">Fecha de retiro:</span>
                                        <span className="value">{bloodRequest.dateOfRetiring}</span>
                                    </p>
                                </div>
                                <div className='blood-request-status-item'>
                                    <ClockCircleOutlined style={{ fontSize: '24px', color: '#BF2C32' }} />
                                    <p>
                                        <span className="label">Hora de retiro:</span>
                                        <span className="value">{bloodRequest.hourOfRetiring}</span>
                                    </p>
                                </div>
                                <button className='register-button' style={{ margin: '10px' }} onClick={() => navigate(`/viewBanks/viewBankDetails/${bloodRequest.bankId}`)}>Contactar Banco</button>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
            <Footer />
        </div>
    )
}

export default ViewBloodRequestStatus;