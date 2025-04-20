from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.support.ui import Select

class RequestBloodPage:
    """Page Object para la página de solicitud de sangre."""
    
    # Localizadores de elementos UI basados en la estructura de requestBlood.jsx
    BLOOD_BANK_SELECT = (By.ID, "bloodBank")
    BLOOD_TYPE_SELECT = (By.ID, "bloodType")
    QUANTITY_INPUT = (By.ID, "quantity")
    REASON_TEXTAREA = (By.ID, "reason")
    SUBMIT_BUTTON = (By.CSS_SELECTOR, ".submit-button")
    NOTIFICATION = (By.CSS_SELECTOR, ".notification")
    
    def __init__(self, driver):
        self.driver = driver
        self.wait = WebDriverWait(driver, 10)
    
    def navigate(self, url):
        """Navegar a la URL de solicitud de sangre."""
        self.driver.get(url)
    
    def is_request_blood_page(self):
        """Verificar si estamos en la página de solicitud de sangre."""
        try:
            return self.driver.find_element(*self.BLOOD_BANK_SELECT).is_displayed()
        except:
            return False
    
    def fill_request_form(self, bank_name, blood_type, quantity, reason):
        """Completar el formulario de solicitud de sangre."""
        # Seleccionar banco de sangre
        select_bank = Select(self.driver.find_element(*self.BLOOD_BANK_SELECT))
        select_bank.select_by_visible_text(bank_name)
        
        # Seleccionar tipo de sangre
        select_blood_type = Select(self.driver.find_element(*self.BLOOD_TYPE_SELECT))
        select_blood_type.select_by_visible_text(blood_type)
        
        # Ingresar cantidad
        self.driver.find_element(*self.QUANTITY_INPUT).send_keys(quantity)
        
        # Ingresar motivo
        self.driver.find_element(*self.REASON_TEXTAREA).send_keys(reason)
    
    def submit_request(self):
        """Enviar la solicitud."""
        self.driver.find_element(*self.SUBMIT_BUTTON).click()
    
    def get_notification_message(self):
        """Obtener mensaje de notificación después de enviar solicitud."""
        try:
            self.wait.until(EC.visibility_of_element_located(self.NOTIFICATION))
            return self.driver.find_element(*self.NOTIFICATION).text
        except:
            return None