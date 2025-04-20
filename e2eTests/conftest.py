import pytest
from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from webdriver_manager.chrome import ChromeDriverManager
from config.config import IMPLICIT_WAIT

@pytest.fixture(scope="function")
def driver():
    # Path to your manually downloaded ChromeDriver
    chrome_driver_path = "C:\\WebDrivers\\chromedriver.exe"  # Update this path to where you saved it
    
    options = webdriver.ChromeOptions()
    options.add_argument("--start-maximized")
    options.add_argument("--disable-extensions")
    options.add_argument("--disable-notifications")
    
    service = Service(chrome_driver_path)
    driver = webdriver.Chrome(service=service, options=options)
    
    driver.implicitly_wait(IMPLICIT_WAIT)
    yield driver
    driver.quit()