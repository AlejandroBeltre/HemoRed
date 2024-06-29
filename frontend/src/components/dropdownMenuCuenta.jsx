import React from 'react';
import './dropdownMenuConsultas.css';
import { Link } from 'react-router-dom';

const Dropdown = ({ visible }) => {
  if (!visible) return null;
  return (
    <div className="dropdown-menu">
      <ul>
        <li><Link to="registerUser" style={{textDecoration: 'none', color: 'inherit'}}>Registro</Link></li>
      </ul>
    </div>
  ); 
};

export default Dropdown;