from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

class LoginPage:
    """Page Object para la página de inicio de sesión."""
    
    # Localizadores de elementos UI basados en la estructura de loginUser.jsx
    EMAIL_INPUT = (By.ID, "email")
    PASSWORD_INPUT = (By.ID, "password")
    LOGIN_BUTTON = (By.CSS_SELECTOR, ".submit-button")
    NOTIFICATION = (By.CSS_SELECTOR, ".notification")
    
    def __init__(self, driver):
        self.driver = driver
        self.wait = WebDriverWait(driver, 10)
    
    def navigate(self, url):
        """Navegar a la URL de inicio de sesión."""
        self.driver.get(url)
    
    def login(self, email, password):
        """Realizar el proceso de inicio de sesión."""
        # Ingresar credenciales
        self.driver.find_element(*self.EMAIL_INPUT).send_keys(email)
        self.driver.find_element(*self.PASSWORD_INPUT).send_keys(password)
        
        # Hacer clic en el botón de inicio de sesión
        login_button = self.driver.find_element(*self.LOGIN_BUTTON)
        login_button.click()
        
        # Esperar a que se complete el inicio de sesión (esperar que desaparezca la notificación o redireccionamiento)
        try:
            self.wait.until(lambda d: "/loginUser" not in d.current_url)
            return True
        except:
            return False
    
    def is_login_page(self):
        """Verificar si estamos en la página de inicio de sesión."""
        try:
            return self.driver.find_element(*self.EMAIL_INPUT).is_displayed()
        except:
            return False