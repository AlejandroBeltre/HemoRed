import React from 'react';
import './dropdownMenuConsultas.css';

const Dropdown = ({ visible }) => {
  if (!visible) return null;
  return (
    <div className="dropdown-menu">
      <ul>
        <li>Submenu 1</li>
        <li>Submenu 2</li>
        <li>Submenu 3</li>
      </ul>
    </div>
  );
};

export default Dropdown;