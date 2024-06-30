import React, { useState } from 'react'
import './viewDonationHistory.css'
import Headers from '../components/header.jsx'
import Footer from '../components/footer.jsx'
import { ArrowLeftOutlined} from '@ant-design/icons'
import { Link, useNavigate } from 'react-router-dom'

function ViewDonationHistory(){
    const donations = [
        { date: 'AGOSTO, 2023', day: 15, type: 'Donación de Sangre' },
        { date: 'MARZO, 2019', day: 11, type: 'Donación de Sangre' },
        { date: 'ABRIL, 2018', day: 21, type: 'Donación de Sangre' },
        { date: 'JULIO, 2015', day: 3, type: 'Plasma y Plaquetas' },
        { date: 'AGOSTO, 2012', day: 30, type: 'Plaquetas' },
      ];
      const navigate = useNavigate();
      const handleBack = () => {
        navigate(-1);
      }
    return(
        <div>
      <Headers />
      <div className="donation-history">
        <ArrowLeftOutlined className="back" onClick={handleBack}/>
        <div className="donation-content">
          <div className="donation-history-info">
            <h1>Mis donaciones</h1>
            <div className="medal">🏅</div>
            <div className="user-details">
              <h2>Juan R. Martinez S.</h2>
              <p className="user-id">ID: 00000001</p>
              <p className="quote">
                "Cada donación de sangre es un acto de amor que salva vidas. Tu generosidad sigue siendo el regalo más valioso para quienes más lo necesitan."
              </p>
            </div>
          </div>
          <div className="donation-list">
            {donations.map((donation, index) => (
                <div key={index} className={`donation-item-container ${index === donations.length - 1 ? 'last-item' : ''}`}>
                <div className="donation-item">
                    <div className="donation-amount">{donation.day}</div>
                    <div className="donation-details">
                    <p className="donation-date">{donation.date}</p>
                    <p className="donation-type">{donation.type}</p>
                    </div>
                </div>
                </div>
            ))}
            </div>
        </div>
      </div>
      <Footer />
    </div>
    )
}

export default ViewDonationHistory;