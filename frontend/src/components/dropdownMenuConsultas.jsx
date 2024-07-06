import React from 'react';
import './dropdownMenuConsultas.css';
import { Link } from 'react-router-dom';

const Dropdown = ({ visible }) => {
  if (!visible) return null;
  return (
    <div className="dropdown-menu">
      <ul>
        <li><Link to="/viewDonationHistory" style={{textDecoration: 'none', color: 'inherit'}}>Historial de Donaciones</Link></li>
        <li><Link to="/viewBanks" style={{textDecoration: 'none', color: 'inherit'}}>Bancos de Sangre</Link></li>
      <li><Link to="/viewBloodRequestStatus" style={{textDecoration: 'none', color: 'inherit'}}>Solicitudes de Sangre</Link></li>
      </ul>
    </div>
  );
};

export default Dropdown;