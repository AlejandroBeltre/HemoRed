import React from 'react';
import './manageBloodInventory.css';
import Headers from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { Link, useNavigate } from 'react-router-dom';


const BloodTypeDisplay = ({type, bags}) => {
    return(
    <div className='blood-type-display'>
        <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#BF2C32">
            <path d="M480-80q-137 0-228.5-94T160-408q0-100 79.5-217.5T480-880q161 137 240.5 254.5T800-408q0 140-91.5 234T480-80Zm0-80q104 0 172-70.5T720-408q0-73-60.5-165T480-774Q361-665 300.5-573T240-408q0 107 68 177.5T480-160Zm-120-80h240v-80H360v80Zm80-120h80v-80h80v-80h-80v-80h-80v80h-80v80h80v80Zm40-120Z"/>
        </svg>
        <span className='blood-type-bank-manage'>{type}:</span>
        <span className='blood-bags'>{bags}</span>
    </div>
    );
}

const BloodBankCard = ({ bank }) => {
    return (
      <div className="blood-bank-card">
        <div className="card-header">
          <h2 className="blood-inventory-bank-name">{bank.name}</h2>
          <span className={`bank-status ${bank.isActive ? 'status-active' : 'status-inactive'}`}>
            {bank.isActive ? 'Activo' : 'Inactivo'}
          </span>
        </div>
        <div className="blood-inventory-card-content">
          <p className="bank-info">{bank.address}</p>
          <p className="bank-info">{bank.phoneNumber}</p>
          <div className="blood-types-grid">
            {bank.bloodTypes.map((bloodType) => (
              <BloodTypeDisplay key={bloodType.type} type={bloodType.type} bags={bloodType.bags} />
            ))}
          </div>
        </div>
        <button className="edit-button">Editar</button>
      </div>
    );
  };
  

function ManageBloodInventory() {
    const allBloodTypes = ["A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"];
    const bloodBanks = [
        {
          id: 1,
          name: "Blood Bank of Alaska",
          isActive: true,
          address: "1215 Airport Heights Dr, Anchorage, AK 99508, USA",
          phoneNumber: "+1 (907) 222-5630",
          bloodTypes: [
            { type: "A+", bags: 23 },
            { type: "A-", bags: 100 },
            { type: "B+", bags: 23 },
            { type: "B-", bags: 100 },
            { type: "AB+", bags: 23 },
            { type: "AB-", bags: 100 },
            { type: "O+", bags: 23 },
            { type: "O-", bags: 0 },
          ]
        },
        {
          id: 2,
          name: "Centro de la sangre y especialidades",
          isActive: false,
          address: "123 Main St, Cityville",
          phoneNumber: "123-456-7890",
          bloodTypes: [
            { type: "A+", bags: 120 },
            { type: "A-", bags: 0 },
            { type: "B+", bags: 95 },
            { type: "B-", bags: 0 },
            { type: "AB+", bags: 0 },
            { type: "AB-", bags: 0 },
            { type: "O+", bags: 0 },
            { type: "O-", bags: 0 },
          ]
        },
        {
            id: 1,
            name: "Blood Bank of Alaska",
            isActive: true,
            address: "1215 Airport Heights Dr, Anchorage, AK 99508, USA",
            phoneNumber: "+1 (907) 222-5630",
            bloodTypes: [
              { type: "A+", bags: 23 },
              { type: "A-", bags: 100 },
              { type: "B+", bags: 23 },
              { type: "B-", bags: 100 },
              { type: "AB+", bags: 23 },
              { type: "AB-", bags: 100 },
              { type: "O+", bags: 23 },
              { type: "O-", bags: 0 },
            ]
          }
      ];
        const navigate = useNavigate();
        const handleBack = () => {
            navigate(-1);
        };
    return (
        <div>
            <Headers />
            <div className='view-blood-request-status-container'>
                <ArrowLeftOutlined className='back' onClick={handleBack} />
                <h1 className='inventory-title'>Inventory Management</h1>
                <div className='blood-bank-inventory-grid'>
                    {bloodBanks.map((bank) => (
                        <BloodBankCard key={bank.id} bank={bank} />
                    ))}
                </div>
            </div>
            <Footer />
        </div>
    );
}

export default ManageBloodInventory;