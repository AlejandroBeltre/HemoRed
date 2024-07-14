import React, { useState, useContext } from 'react'
import './viewDonationHistory.css'
import Headers from '../components/header.jsx'
import Footer from '../components/footer.jsx'
import { ArrowLeftOutlined } from '@ant-design/icons'
import { Link, useNavigate } from 'react-router-dom'
import { UserContext } from '../UserContext';

function ViewDonationHistory() {
    const { user } = useContext(UserContext);
    const donations = [];
    const navigate = useNavigate();
    const handleBack = () => {
        navigate(-1);
    }
    
    return (
        <div>
            <Headers />
            <ArrowLeftOutlined className="back" onClick={handleBack} />
            <div className="donation-history">
                <div className="donation-content">
                    <div className="donation-history-info">
                        <h1>Mis donaciones</h1>
                        <div className="user-details">
                            <h2>{user.fullName}</h2>
                            <p className="user-id">ID: {user.documentNumber}</p>
                            <p className="quote">
                                "Cada donación de sangre es un acto de amor que salva vidas. Tu generosidad sigue siendo el regalo más valioso para quienes más lo necesitan."
                            </p>
                        </div>
                    </div>
                    <div className="donation-list">
                        {donations.length === 0 ? (
                            <div className="no-donations">
                                <p>No hay donaciones para mostrar.</p>
                            </div>
                        ) : (
                            donations.map((donation, index) => (
                                <div key={index} className={`donation-item-container ${index === donations.length - 1 ? 'last-item' : ''}`}>
                                    <div className="donation-item">
                                        <div className="donation-amount">{donation.day}</div>
                                        <div className="donation-details">
                                            <p className="donation-date">{donation.date}</p>
                                            <p className="donation-type">{donation.type}</p>
                                        </div>
                                    </div>
                                </div>
                            ))
                        )}
                    </div>
                </div>
            </div>
            <Footer />
        </div>
    )
}

export default ViewDonationHistory;