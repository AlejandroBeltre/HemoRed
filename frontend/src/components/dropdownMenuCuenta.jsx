import React from 'react';
import './dropdownMenuConsultas.css';
import { Link, useNavigate } from 'react-router-dom';

const Dropdown = ({ visible }) => {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem('userToken');
    localStorage.removeItem('userRole');
    localStorage.removeItem('userID');
    localStorage.removeItem('userName');
    localStorage.removeItem('userLastName');
    localStorage.removeItem('userEmail');
    localStorage.removeItem('userPhone');
    localStorage.removeItem('userAddress');
    localStorage.removeItem('userBloodType');
    navigate('/');
  };
  if (!visible) return null;
  return (
    <div className="dropdown-menu">
      <ul>
        <li><Link to="/registerUser" style={{textDecoration: 'none', color: 'inherit'}}>Registrarme</Link></li>
        <li><Link to="/loginUser" style={{textDecoration: 'none', color: 'inherit'}}>Iniciar sesión</Link></li>
        <li><Link to="/modifyProfile" style={{textDecoration: 'none', color: 'inherit'}}>Editar perfil</Link></li>
        <li><Link to="/" onClick={handleLogout} style={{textDecoration: 'none', color: 'inherit'}}>Cerrar sesión</Link></li>
      </ul>
    </div>
  ); 
};

export default Dropdown;