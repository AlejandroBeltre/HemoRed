import React, { useState, useContext, useEffect } from 'react';
import DropdownConsultas from './dropdownMenuConsultas';
import DropdownSolicitudes from './dropdownMenuSolicitudes';
import DropdownGestion from './dropdownMenuGestion';
import DropdownCampañas from './dropdownMenuCampañas';
import DropdownCuenta from './dropdownMenuCuenta';
import DownOutlined from '@ant-design/icons/DownOutlined';
import { Link } from 'react-router-dom';
import logo from '../assets/images/HemoRedLogo.png';
import './header.css';
import { UserContext } from '../UserContext';

function Headers() {
    const [activeDropdown, setActiveDropdown] = useState(null);
    const { user } = useContext(UserContext);
    const [userRole, setUserRole] = useState(null);

    useEffect(() => {
        if (user) {
            setUserRole(user.userRole);
        }
    }, [user]);

    const handleMouseEnter = (menu) => {
        setActiveDropdown(menu);
    };

    const handleMouseLeave = () => {
        setActiveDropdown(null);
    };

    return (
        <header>
            <div className="container">
                <Link to="/" style={{ textDecoration: 'none', color: 'inherit' }}>
                    <img src={logo} alt="Logo" className='logo' />
                </Link>
                <nav>
                    <ul>
                        <li
                            onMouseEnter={() => handleMouseEnter('consultas')}
                            onMouseLeave={handleMouseLeave}
                        >
                            Consultas
                            <DownOutlined className='down-icon' />
                            <DropdownConsultas visible={activeDropdown === 'consultas'} />
                        </li>
                        <li
                            onMouseEnter={() => handleMouseEnter('solicitudes')}
                            onMouseLeave={handleMouseLeave}
                        >
                            Solicitudes
                            <DownOutlined className='down-icon' />
                            <DropdownSolicitudes visible={activeDropdown === 'solicitudes'} />
                        </li>
                        {userRole === 0 && (
                            <li
                                onMouseEnter={() => handleMouseEnter('gestion')}
                                onMouseLeave={handleMouseLeave}
                            >
                                Gestión
                                <DownOutlined className='down-icon' />
                                <DropdownGestion visible={activeDropdown === 'gestion'} />
                            </li>
                        )}
                        <li
                            onMouseEnter={() => handleMouseEnter('campañas')}
                            onMouseLeave={handleMouseLeave}
                        >
                            Campañas
                            <DownOutlined className='down-icon' />
                            <DropdownCampañas visible={activeDropdown === 'campañas'} />
                        </li>
                        <li
                            onMouseEnter={() => handleMouseEnter('cuenta')}
                            onMouseLeave={handleMouseLeave}
                        >
                            Cuenta
                            <DownOutlined className='down-icon' />
                            <DropdownCuenta visible={activeDropdown === 'cuenta'} />
                        </li>
                    </ul>
                </nav>
            </div>
        </header>
    );
}

export default Headers;