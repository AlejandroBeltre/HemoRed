import React, { useState } from 'react';
import './dropdownMenu.css';

const Dropdown = ({ visible }) => {
  if (!visible) return null;
    return (
        <div className="dropdown-menu">
          <ul>
            <li>Menu 1</li>
            <li>Menu 2</li>
            <li>Menu 3</li>
          </ul>
        </div>
      );
};

export default Dropdown;