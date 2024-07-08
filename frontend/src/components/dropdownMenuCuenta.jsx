import React from 'react';
import './dropdownMenuConsultas.css';
import { Link } from 'react-router-dom';

const Dropdown = ({ visible }) => {
  if (!visible) return null;
  return (
    <div className="dropdown-menu">
      <ul>
        <li><Link to="/registerUser" style={{textDecoration: 'none', color: 'inherit'}}>Registrarme</Link></li>
        <li><Link to="/loginUser" style={{textDecoration: 'none', color: 'inherit'}}>Iniciar sesión</Link></li>
        <li><Link to="/modifyProfile" style={{textDecoration: 'none', color: 'inherit'}}>Editar perfil</Link></li>
        <li><Link to="/dashboardUser" style={{textDecoration: 'none', color: 'inherit'}}>Dashboard</Link></li>
        <li><Link to="/dashboardAdmin" style={{textDecoration: 'none', color: 'inherit'}}>Dashboard</Link></li>
        <li><Link to="/" style={{textDecoration: 'none', color: 'inherit'}}>Cerrar sesión</Link></li>
      </ul>
    </div>
  ); 
};

export default Dropdown;