import React from 'react';
import './dropdownMenuConsultas.css';
import { Link } from 'react-router-dom';

const Dropdown = ({ visible }) => {
  if (!visible) return null;
  return (
    <div className="dropdown-menu">
      <ul>
      <li><Link to="/requestBlood" style={{textDecoration: 'none', color: 'inherit'}}>Solicitar Sangre</Link></li>
      <li><Link to="/scheduleAppointment" style={{textDecoration: 'none', color: 'inherit'}}>Donar Sangre</Link></li>
      </ul>
    </div>
  );
};

export default Dropdown;