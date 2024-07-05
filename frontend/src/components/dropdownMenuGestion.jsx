import React from 'react';
import './dropdownMenuConsultas.css';
import { Link } from 'react-router-dom';

const Dropdown = ({ visible }) => {
  if (!visible) return null;
  return (
    <div className="dropdown-menu">
      <ul>
      <li><Link to="/manageBloodInventory" style={{textDecoration: 'none', color: 'inherit'}}>Gestionar Inventario de Sangre</Link></li>
      <li><Link to="/manageBloodBanks" style={{textDecoration: 'none', color: 'inherit'}}>Gestionar Bancos de Sangre</Link></li>
      </ul>
    </div>
  );
};

export default Dropdown;