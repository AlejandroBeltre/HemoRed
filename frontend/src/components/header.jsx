import React, { useState } from 'react'
import Dropdown from './dropdownMenu'
import DownOutlined from '@ant-design/icons/DownOutlined'
import './header.css'

function Headers() {
    const [activeDropdown, setActiveDropdown] = useState(null);

    const handleMouseEnter = (menu) => {
        setActiveDropdown(menu);
    };

    const handleMouseLeave = () => {
        setActiveDropdown(null);
    };

    return (
        <header>
            <div className="container">
                <h1>Herr Red</h1>
                <nav>
                    <ul>
                        <li 
                            onMouseEnter={() => handleMouseEnter('consultas')} 
                            onMouseLeave={handleMouseLeave}
                        >
                            Consultas
                            <DownOutlined className='down-icon' />
                            <Dropdown visible={activeDropdown === 'consultas'} />
                        </li>
                        <li 
                            onMouseEnter={() => handleMouseEnter('solicitudes')} 
                            onMouseLeave={handleMouseLeave}
                        >
                            Solicitudes
                            <Dropdown visible={activeDropdown === 'solicitudes'} />
                        </li>
                        <li 
                            onMouseEnter={() => handleMouseEnter('gestion')} 
                            onMouseLeave={handleMouseLeave}
                        >
                            Gestión
                            <Dropdown visible={activeDropdown === 'gestion'} />
                        </li>
                        <li 
                            onMouseEnter={() => handleMouseEnter('campanas')} 
                            onMouseLeave={handleMouseLeave}
                        >
                            Campañas
                            <Dropdown visible={activeDropdown === 'campanas'} />
                        </li>
                        <li 
                            onMouseEnter={() => handleMouseEnter('cuenta')} 
                            onMouseLeave={handleMouseLeave}
                        >
                            Cuenta
                            <Dropdown visible={activeDropdown === 'cuenta'} />
                        </li>
                    </ul>
                </nav>
            </div>
        </header>
    )
}

export default Headers