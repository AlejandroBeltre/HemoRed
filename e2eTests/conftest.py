import pytest
from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from webdriver_manager.chrome import ChromeDriverManager
from config.config import IMPLICIT_WAIT

@pytest.fixture(scope="function")
def driver():
    """Fixture para configurar y proporcionar un WebDriver para las pruebas."""
    # Configurar opciones del navegador
    options = webdriver.ChromeOptions()
    
    # Comentar la siguiente línea para ver el navegador durante las pruebas
    # options.add_argument("--headless")
    
    options.add_argument("--start-maximized")
    options.add_argument("--disable-extensions")
    options.add_argument("--disable-notifications")
    
    # Inicializar el WebDriver
    service = Service(ChromeDriverManager().install())
    driver = webdriver.Chrome(service=service, options=options)
    
    # Configurar tiempo de espera implícito
    driver.implicitly_wait(IMPLICIT_WAIT)
    
    # Proporcionar el driver para la prueba
    yield driver
    
    # Limpieza: cerrar el navegador después de cada prueba
    driver.quit()