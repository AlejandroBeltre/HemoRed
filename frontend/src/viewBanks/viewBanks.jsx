import React, { useState, useEffect } from 'react';
import './viewBanks.css';
import Headers from '../components/header.jsx';
import Footer from '../components/footer.jsx';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { Link, useNavigate } from 'react-router-dom';
import { getBloodBanks, getAddressById } from '../api.js';

function ViewBanks() {
  const [bloodBanks, setBloodBanks] = useState([]);
  const [addresses, setAddresses] = useState({});
  const [selectedCity, setSelectedCity] = useState('');
  const [selectedBloodType, setSelectedBloodType] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchBloodBanks = async () => {
      try {
        const response = await getBloodBanks();
        setBloodBanks(response.data);
        console.log(response.data);

        // Fetch addresses
        const addressPromises = response.data.map(bank => getAddressById(bank.addressID));
        const addressResponses = await Promise.all(addressPromises);
        const addressData = addressResponses.reduce((acc, addressResponse) => {
          acc[addressResponse.data.addressID] = addressResponse.data;
          return acc;
        }, {});
        setAddresses(addressData);
      } catch (error) {
        console.error('Error fetching blood banks:', error);
      }
    };

    fetchBloodBanks();
  }, []);

  const handleBack = () => {
    navigate(-1);
  };

  return (
    <div>
      <Headers />
      <ArrowLeftOutlined className='back' onClick={handleBack} />
      <div className="blood-banks-container">
        <h1>Bancos de sangre</h1>
        <p>Encuentra los bancos en tu área y filtralos por el tipo de sangre que te interese.</p>

        <div className="filters">
          <select
            value={selectedCity}
            onChange={(e) => setSelectedCity(e.target.value)}
            className="city-select"
          >
            <option value="">Ciudad</option>
            <option value="Santo Domingo">Santo Domingo</option>
            <option value="Santiago">Santiago</option>
          </select>
        </div>
        <div className="blood-banks-list">
          {bloodBanks && bloodBanks.map((bank, index) => (
            <div key={index} className="blood-bank-card">
              <div className="card-content-blood-bank">
                {bank.image ? (
                  <img src={bank.image} alt={bank.bloodBankName + ' logo'} className="blood-bank-logo" />
                ) : (
                  <div className="no-image">Imagen no disponible</div>
                )}
                <h2>{bank.bloodBankName}</h2>
                <p>Dirección: {addresses[bank.addressID] ? addresses[bank.addressID].street + ', ' + addresses[bank.addressID].buildingNumber : 'Loading...'}</p>
                <p>Teléfono: {bank.phone}</p>
                <p>Horas Disponibles: {bank.availableHours}</p>
              </div>
              <button className="location-button" onClick={() => navigate(`/viewBanks/viewBankDetails/${bank.bloodBankID}`)}>Ubicación</button>
            </div>
          ))}
        </div>
      </div>
      <Footer />
    </div>
  );
}

export default ViewBanks;