import React, {useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Instagram, Facebook, Twitter, MessageCircle } from 'lucide-react';
import '../App.css';
import './homePage.css';
import Dropdown from '../components/dropdownMenu';

function HomePage(){
    const [isDropdownVisible, setDropdownVisible] = useState(false);
        return(
        <div className="font-sans">
          {/* Header */}
          <header className="bg-red-500 text-white p-4">
            <div className="container mx-auto flex justify-between items-center">
              <h1 className="text-2xl font-bold">Herr Red</h1>
              <nav>
                <ul className="flex space-x-4">
                <li onMouseEnter={() => setDropdownVisible(true)} onMouseLeave={() => setDropdownVisible(false)}>
                    Consultas
                    <Dropdown visible={isDropdownVisible} />
                  </li>
                  <li>Solicitudes</li>
                  <li>Gestión</li>
                  <li>Campañas</li>
                  <li>Cuenta</li>
                </ul>
              </nav>
            </div>
          </header>

          {/* Main Content */}
          <main className="container mx-auto mt-8">
            {/* Hero Section */}
            <section className="hero-section">
              {/* Placeholder for the image */}
              <div className="absolute inset-0 bg-cover bg-center" style={{backgroundImage: "url('/path/to/your/image.jpg')"}}></div>
            </section>

            {/* Blood Types */}
            <section className="blood-types">
              <h2 className="text-2xl font-bold mb-4">TIPOS DE SANGRE</h2>
              <div className="blood-type-grid">
                {['A', 'B', 'AB', 'O'].map(type => (
                  <div key={type} className="blood-type-icon flex items-center justify-center">
                    <img src={`/path/to/${type.toLowerCase()}-blood-icon.png`} alt={`Blood type ${type}`} className="w-16 h-16" />
                  </div>
                ))}
              </div>
            </section>

            {/* Donation CTA */}
            <section className="donation-cta bg-gray-100 p-6 flex items-center mb-8">
              <div className="donation-image flex-1">
                <img src="/path/to/donation-image.jpg" alt="Blood donation" className="w-full h-auto" />
              </div>
              <div className="donation-text flex-1 pl-6">
                <p className="text-lg mb-4">Con tu donación, puedes transformar el futuro de quienes más lo necesitan. ¡Haz la diferencia hoy!</p>
                <button className="btn btn-primary px-6 py-2 rounded">Donar</button>
              </div>
            </section>

            {/* Testimonials */}
            <section className="testimonials mb-8">
              <h2 className="text-2xl font-bold mb-4">TESTIMONIOS</h2>
              <div className="testimonial-grid">
                {[1, 2, 3].map(i => (
                  <div key={i} className="testimonial text-center">
                    <img src={`/path/to/testimonial-${i}.jpg`} alt={`Testimonial ${i}`} className="w-24 h-24 rounded-full mx-auto mb-2" />
                    <p className="text-sm">Saber que mi pequeña contribución puede salvar vidas me llena de alegría y gratitud.</p>
                  </div>
                ))}
              </div>
            </section>

            {/* Need Blood Section */}
            <section className="need-blood bg-gray-100 p-6 flex items-center mb-8">
              <div className="need-blood-image flex-1">
                <img src="/path/to/need-blood-image.jpg" alt="Need blood" className="w-full h-auto" />
              </div>
              <div className="need-blood-text flex-1 pl-6">
                <h2 className="text-2xl font-bold mb-4">¿NECESITAS SANGRE?</h2>
                <p className="mb-4">"Para quienes enfrentan situaciones críticas de salud, comprar sangre no es solo una necesidad, sino una esperanza de vida. Cada gota cuenta, y tu ayuda puede marcar la diferencia en su lucha por sobrevivir."</p>
                <button className="btn btn-secondary px-6 py-2 rounded">Adquirir sangre</button>
              </div>
            </section>

            {/* Quote */}
            <section className="quote text-center mb-8">
              <p className="text-lg italic">"Donar sangre no solo salva vidas; es un acto de solidaridad que fortalece nuestra comunidad y ofrece esperanza a quienes más lo necesitan."</p>
            </section>
          </main>

          {/* Footer */}
          <footer className="bg-red-500 text-white p-8">
            <div className="container mx-auto">
              <div className="footer-content flex justify-between items-center">
                <div>
                  <h2 className="text-2xl font-bold mb-4">Herr Red</h2>
                  <ul>
                    <li>DONAR</li>
                    <li>COMPRAR SANGRE</li>
                    <li>CAPACITACIONES</li>
                  </ul>
                </div>
                <div className="social-icons flex space-x-4">
                  <Instagram />
                  <Facebook />
                  <Twitter />
                  <MessageCircle />
                </div>
              </div>
              <div className="footer-bottom mt-8 text-center">
                <p>Grupo 02 | 2024 | Aseguramiento de la calidad</p>
              </div>
            </div>
          </footer>
        </div>
        )
    }

export default HomePage;
