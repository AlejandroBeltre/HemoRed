from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

class RequestStatusPage:
    """Page Object para la página de estado de solicitudes."""
    
    # Localizadores de elementos UI basados en la estructura de viewBloodRequestStatus.jsx
    REQUEST_BLOCKS = (By.CSS_SELECTOR, ".blood-request-status-block")
    CONTACT_BANK_BUTTON = (By.CSS_SELECTOR, ".contact-bank-button")
    
    def __init__(self, driver):
        self.driver = driver
        self.wait = WebDriverWait(driver, 10)
    
    def navigate(self, url):
        """Navegar a la URL de estado de solicitudes."""
        self.driver.get(url)
    
    def find_request_by_blood_type(self, blood_type):
        """Encontrar una solicitud específica por tipo de sangre."""
        # Localizar bloques de solicitud
        request_blocks = self.driver.find_elements(*self.REQUEST_BLOCKS)
        
        # Buscar en cada bloque el tipo de sangre específico
        for block in request_blocks:
            try:
                blood_type_element = block.find_element(By.XPATH, f".//p[contains(text(), '{blood_type}')]")
                if blood_type_element:
                    return block  # Retorna el bloque que contiene el tipo de sangre
            except:
                continue
        
        return None  # No se encontró la solicitud
    
    def click_contact_bank_button(self, request_block):
        """Hacer clic en el botón 'Contactar Banco' para una solicitud específica."""
        try:
            contact_button = request_block.find_element(By.CSS_SELECTOR, ".contact-bank-button")
            contact_button.click()
            return True
        except:
            return False