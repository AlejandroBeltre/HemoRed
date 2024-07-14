import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import Headers from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined } from '@ant-design/icons';
import './viewBankDetails.css';
import map from "../assets/images/map.png";
import { getBloodBanks, getAllBloodBanks } from "../api";
import { getAddressById } from "../api";

function ViewBankDetails() {
  const navigate = useNavigate();
  const location = useLocation();
  const { bank } = location.state || {};
  const [bankDetails, setBankDetails] = useState(bank);
  const [address, setAddress] = useState(null);
  const [formattedAddress, setFormattedAddress] = useState('');
  const handleBack = () => {
    navigate(-1);
  }

  useEffect(() => {
    if (!bankDetails) {
      navigate('/viewBanks');
    } else {
      const fetchAddress = async () => {
        try {
          const response = await getAddressById(bankDetails.addressID);
          setAddress(response.data);
          setFormattedAddress(`${response.data.street}, ${response.data.buildingNumber}`);
        } catch (error) {
          console.error('Error fetching address:', error);
        }
      };

      fetchAddress();
    }
  }, [bankDetails, navigate]);

  const navigateToRequestBloodPage = () => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    navigate('/requestBlood', { state: { bankName: bankDetails.name } });
  }

  const navigateToDonateBloodPage = () => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    navigate('/scheduleAppointment', { state: { bankName: bankDetails.name } });
  }

  return (
    <div className="viewBankDetails">
      <Headers />
      <ArrowLeftOutlined onClick={handleBack} className="back" />
      <div className="viewBankDetails-content">
        <div className="viewBankDetails-body">
          <div className="left-column">
            <h1>{bankDetails.name}</h1>
            <img src={bankDetails.image} alt={bankDetails.name} className='bank-detail-logo' />
            <div className="blood-types">
              <h3>TIPOS DE SANGRE DISPONIBLE:</h3>
              <span className='blood-types'>A+, A-, B+, B-, AB+, AB-, O+, O-.</span>
            </div>
            <div className="buttons-banks">
              <button className="donor-button" onClick={navigateToDonateBloodPage}>Donar</button>
              <button className="acquire-button" onClick={navigateToRequestBloodPage}>Adquirir sangre</button>
            </div>
            <div className="info-box">
              <h3>Información</h3>
              <p>{address ? `${address.street}, ${address.buildingNumber}` : 'Loading...'}</p>
              <p>{bankDetails.phone}</p>
              <p>{bankDetails.availableHours}</p>
            </div>
          </div>
          <div className="right-column">
            <div className="map">
              {formattedAddress && (
                <iframe
                  width="100%"
                  height="450"
                  style={{ border: 0, borderRadius: '10px', border: '1px solid #000000' }}
                  src={`https://www.google.com/maps/embed/v1/place?key=AIzaSyD1d4ZG7FpkNj-WF1_0DZHxhqQO3WwCjzM&q=${encodeURIComponent(formattedAddress)}`}
                  allowFullScreen
                ></iframe>
              )}
            </div>
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
}


export default ViewBankDetails;