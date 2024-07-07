import React from 'react';
import './dropdownMenuConsultas.css';
import { Link } from 'react-router-dom';

const Dropdown = ({ visible }) => {
  if (!visible) return null;
  return (
    <div className="dropdown-menu">
      <ul>
        <li><Link to="/campaigns" style={{textDecoration: 'none', color: 'inherit'}}>Campañas activas</Link></li>
        <li><Link to="/campaigns/participateCampaign" style={{textDecoration: 'none', color: 'inherit'}}>Participar en campaña</Link></li>
      </ul>
    </div>
  );
};

export default Dropdown;