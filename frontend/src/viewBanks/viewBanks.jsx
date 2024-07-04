import React, { useState } from 'react';
import './viewBanks.css';
import Headers from '../components/header.jsx';
import Footer from '../components/footer.jsx';
import { ArrowLeftOutlined } from '@ant-design/icons';
import CentroSangreEspecialidades from "../assets/images/CentroSangreEspecialidades.png";
import Crisney from "../assets/images/CrisNey.png";
import SanDiego from "../assets/images/SanDiego.png";
import NewYork from "../assets/images/NewYork.png";
import Alaska from "../assets/images/Alaska.png";
import Bloodcare from "../assets/images/Bloodcare.png";
import { Link, useNavigate } from 'react-router-dom';

function ViewBanks() {
  const bloodBanks = [
    {
      id: 1,
      name: "Centro de la sangre y especialidades",
      address: "Av. Maipú 1688, Vicente López",
      hours: "Cerrado • 07:00AM - 12:00PM",
      logo: CentroSangreEspecialidades,
      bloodTypes: ["A+", "O-", "B+", "AB+"]
    },
    {
      id: 2,
      name: "Banco de sangre Fundación Crisney",
      address: "Av. Belgrano 1746, CABA",
      hours: "Cerrado • 07:30AM - 5:00PM",
      logo: Crisney,
      bloodTypes: ["A+", "O-", "B+", "AB+"]
    },
    {
      id: 3,
      name: "San Diego Blood Bank",
      address: "3636 Gateway Center Ave Suite 100",
      hours: "Cerrado • 10:00AM - 6:00PM",
      logo: SanDiego,
      bloodTypes: ["A+", "O-", "B+", "AB+"]
    },
    {
      id: 4,
      name: "New York Blood Center",
      address: "619 W 54th St, New York, NY 10019",
      hours: "Cerrado • 7:00AM - 7:00PM",
      logo: NewYork,
      bloodTypes: ["A+", "O-", "B+", "AB+"]
    },
    {
      id: 5,
      name: "Blood Bank of Delmarva",
      address: "100 Hygeia Dr, Newark, DE 19713",
      hours: "Cerrado • 7:00AM - 3:00PM",
      logo: Alaska,
      bloodTypes: ["A+", "O-", "B+", "AB+"]
    },
    {
      id: 6,
      name: "Carter BloodCare",
      address: "2205 Highway 121, Bedford, TX 76021",
      hours: "Cerrado • 8:00AM - 5:00PM",
      logo: Bloodcare,
      bloodTypes: ["A+", "O-", "B+", "AB+"]
    },
  ];
  const navigate = useNavigate();

  const [selectedCity, setSelectedCity] = useState('');
  const [selectedBloodType, setSelectedBloodType] = useState('');
  const handleBack = () => {
    navigate(-1);
  }
  return (
    <div>
      <Headers />
      <ArrowLeftOutlined className='back' onClick={handleBack} />
      <div className="blood-banks-container">
        <h1>Bancos de sangre</h1>
        <p>Encuentra los bancos en tu area y filtralos por el tipo de sangre que te interese.</p>

        <div className="filters">
          <select
            value={selectedCity}
            onChange={(e) => setSelectedCity(e.target.value)}
            className="city-select"
          >
            <option value="">Ciudad</option>
            {/* Add city options here */}
          </select>

          <select
            value={selectedBloodType}
            onChange={(e) => setSelectedBloodType(e.target.value)}
            className="blood-type-select"
          >
            <option value="">Tipo de sangre</option>
            {/* Add blood type options here */}
          </select>
        </div>
        <div className="blood-banks-list">
          {bloodBanks && bloodBanks.map((bank, index) => (
            <div key={index} className="blood-bank-card">
              <div className="card-content-blood-bank">
                <img src={bank.logo} alt={`${bank.name} logo`} className="blood-bank-logo" />
                <h2>{bank.name}</h2>
                <p>{bank.address}</p>
                <div className="blood-types">
                  {bank.bloodTypes && bank.bloodTypes.map((type, i) => (
                    <span key={i} className="blood-type">{type}</span>
                  ))}
                </div>
                <p className="hours">{bank.hours}</p>
              </div>
              <button className="location-button" onClick={() => navigate(`/viewBanks/viewBankDetails/${bank.id}`)}>Ubicación</button>
            </div>
          ))}
        </div>
      </div>
      <Footer />
    </div>
  )
}

export default ViewBanks;