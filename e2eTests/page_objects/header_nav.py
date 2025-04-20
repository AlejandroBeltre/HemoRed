from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

class HeaderNav:
    """Page Object para la navegación mediante el encabezado."""
    
    # Localizadores basados en components/header.jsx
    ACCOUNT_MENU = (By.XPATH, "//li[contains(text(), 'Cuenta')]")
    LOGOUT_OPTION = (By.XPATH, "//a[contains(text(), 'Cerrar sesión')]")
    
    def __init__(self, driver):
        self.driver = driver
        self.wait = WebDriverWait(driver, 10)
    
    def logout(self):
        """Cerrar sesión a través del menú de cuenta."""
        # Hacer clic en el menú de cuenta
        account_menu = self.driver.find_element(*self.ACCOUNT_MENU)
        account_menu.click()
        
        # Esperar y hacer clic en la opción de cerrar sesión
        logout_option = self.wait.until(EC.element_to_be_clickable(self.LOGOUT_OPTION))
        logout_option.click()
        
        # Esperar a que se complete el cierre de sesión (redirección a home)
        self.wait.until(lambda d: "/" == d.current_url or "/loginUser" in d.current_url)