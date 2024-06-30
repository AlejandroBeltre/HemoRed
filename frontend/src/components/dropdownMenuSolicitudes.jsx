import React from 'react';
import './dropdownMenuConsultas.css';
import { Link } from 'react-router-dom';

const Dropdown = ({ visible }) => {
  if (!visible) return null;
  return (
    <div className="dropdown-menu">
      <ul>
      <li><Link to="/viewBanks" style={{textDecoration: 'none', color: 'inherit'}}>Ver Bancos de Sangre</Link></li>
      </ul>
    </div>
  );
};

export default Dropdown;