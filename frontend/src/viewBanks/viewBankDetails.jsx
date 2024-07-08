import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Headers from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined } from '@ant-design/icons'; 
import CentroSangreEspecialidades from "../assets/images/CentroSangreEspecialidades.png";
import Crisney from "../assets/images/CrisNey.png";
import SanDiego from "../assets/images/SanDiego.png";
import NewYork from "../assets/images/NewYork.png";
import Alaska from "../assets/images/Alaska.png";
import Bloodcare from "../assets/images/Bloodcare.png";
import './viewBankDetails.css';
import map from "../assets/images/map.png";

const fetchBankDetailsById = (id) => {
    
  // This is a placeholder. You should replace it with actual data fetching logic.
  const mockBanks = [
    {
        id: '1',
        name: "Centro de la sangre y especialidades",
        address: "Av. Maipú 1688, Vicente López",
        hours: "Cerrado • 07:00AM - 12:00PM",
        phone: "1-800-000-0000",
        logo: CentroSangreEspecialidades,
        bloodTypes: ["A+", "O-", "B+", "AB+"]
      },
      {
        id: '2',
        name: "Banco de sangre Fundación Crisney",
        address: "Av. Belgrano 1746, CABA",
        hours: "Cerrado • 07:30AM - 5:00PM",
        phone: "1-800-000-0000",
        logo: Crisney, 
        bloodTypes: ["A+", "O-", "B+", "AB+"]
      },
      {
        id: '3',
        name: "San Diego Blood Bank",
        address: "3636 Gateway Center Ave Suite 100",
        hours: "Cerrado • 10:00AM - 6:00PM",
        phone: "1-800-000-0000",
        logo: SanDiego,
        bloodTypes: ["A+", "O-", "B+", "AB+"]
      },
      {
        id: '4',
        name: "New York Blood Center",
        address: "619 W 54th St, New York, NY 10019",
        hours: "Cerrado • 7:00AM - 7:00PM",
        phone: "1-800-000-0000",
        logo: NewYork,
        bloodTypes: ["A+", "O-", "B+", "AB+"]
      },
      {
        id: '5',
        name: "Blood Bank of Delmarva",
        address: "100 Hygeia Dr, Newark, DE 19713",
        hours: "Cerrado • 7:00AM - 3:00PM",
        phone: "1-800-000-0000",
        logo: Alaska,
        bloodTypes: ["A+", "O-", "B+", "AB+"]
      },
      {
        id: '6',
        name: "Carter BloodCare",
        address: "2205 Highway 121, Bedford, TX 76021",
        hours: "Cerrado • 8:00AM - 5:00PM",
        phone: "1-800-000-0000",
        logo: Bloodcare,
        bloodTypes: ["A+", "O-", "B+", "AB+"]
      },
  ];
  return mockBanks.find(bank => bank.id === id);
};

function ViewBankDetails() {
    const navigate = useNavigate();
  const handleBack = () => {
    navigate(-1);
  }
  let { id } = useParams();
  const [bankDetails, setBankDetails] = useState(null);

  useEffect(() => {
    const details = fetchBankDetailsById(id);
    setBankDetails(details);
  }, [id]);

  if (!bankDetails) {
    return <div>Loading...</div>; // Or any other loading state
  }

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
          <img src={bankDetails.logo} alt={bankDetails.name} className='bank-detail-logo'/>
          <div className="blood-types">
            <h3>TIPOS DE SANGRE DISPONIBLE:</h3>
            {bankDetails.bloodTypes.map((type, index) => (
                <span key={index} className="blood-type">{type}</span>
            ))}
          </div>
          <div className="buttons-banks">
          <button className="donor-button" onClick={navigateToDonateBloodPage}>Donar</button>
          <button className="acquire-button" onClick={navigateToRequestBloodPage}>Adquirir sangre</button>
          </div>
          <div className="info-box">
            <h3>Información</h3>
            <p>{bankDetails.address}</p>
            <p>{bankDetails.phone}</p>
            <p>{bankDetails.hours}</p>
          </div>
        </div>
        <div className="right-column">
          <div className="map">
            <img src={map} alt="map" className='map'/>
          </div>
        </div>
      </div>
    </div>
    <Footer />
  </div>
  );
}

export default ViewBankDetails;