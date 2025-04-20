import pytest
import time
from page_objects.login_page import LoginPage
from page_objects.request_blood_page import RequestBloodPage
from page_objects.request_status_page import RequestStatusPage
from page_objects.header_nav import HeaderNav
from config.config import (
    LOGIN_URL, REQUEST_BLOOD_URL, REQUEST_STATUS_URL,
    USER_CREDENTIALS, BLOOD_REQUEST_DATA
)

class TestBloodRequestFlow:
    """Pruebas para el flujo de solicitud de sangre."""
    
    def test_successful_blood_request_flow(self, driver):
        """
        Prueba el flujo completo de solicitud de sangre exitoso.
        
        Pasos:
        1. Usuario inicia sesión
        2. Navega al formulario de solicitud de sangre
        3. Completa y envía la solicitud
        4. Verifica notificación de éxito
        5. Cierra sesión y accede como administrador
        6. Verifica que la solicitud aparezca en el panel
        7. Interactúa con la solicitud (contactar banco)
        """
        # 1. Usuario regular inicia sesión
        login_page = LoginPage(driver)
        login_page.navigate(LOGIN_URL)
        assert login_page.login(
            USER_CREDENTIALS["regular"]["email"],
            USER_CREDENTIALS["regular"]["password"]
        ), "Falló el inicio de sesión como usuario regular"
        
        # 2. Navega a la página de solicitud de sangre
        request_page = RequestBloodPage(driver)
        request_page.navigate(REQUEST_BLOOD_URL)
        assert request_page.is_request_blood_page(), "No se cargó la página de solicitud de sangre"
        
        # 3. Completa y envía la solicitud
        request_page.fill_request_form(
            BLOOD_REQUEST_DATA["blood_bank"],
            BLOOD_REQUEST_DATA["blood_type"],
            BLOOD_REQUEST_DATA["quantity"],
            BLOOD_REQUEST_DATA["reason"]
        )
        request_page.submit_request()
        
        # 4. Verifica notificación de éxito
        notification = request_page.get_notification_message()
        assert notification and "¡Solicitud enviada!" in notification, "No se mostró la notificación de éxito"
        
        # 5. Cierra sesión y accede como administrador del banco
        nav = HeaderNav(driver)
        nav.logout()
        
        login_page.navigate(LOGIN_URL)
        assert login_page.login(
            USER_CREDENTIALS["admin"]["email"],
            USER_CREDENTIALS["admin"]["password"]
        ), "Falló el inicio de sesión como administrador del banco"
        
        # 6. Verifica que la solicitud aparezca en el panel
        status_page = RequestStatusPage(driver)
        status_page.navigate(REQUEST_STATUS_URL)
        
        # Esperar un momento para que la página cargue completamente
        time.sleep(2)
        
        # Buscar la solicitud recién creada por tipo de sangre
        request_block = status_page.find_request_by_blood_type(BLOOD_REQUEST_DATA["blood_type"])
        assert request_block is not None, f"No se encontró la solicitud con tipo de sangre {BLOOD_REQUEST_DATA['blood_type']}"
        
        # 7. Contactar al banco para esta solicitud (simular el inicio del procesamiento)
        assert status_page.click_contact_bank_button(request_block), "No se pudo hacer clic en el botón 'Contactar Banco'"
        
        # La prueba pasa si llega hasta aquí sin errores
        
    def test_unauthenticated_user_blood_request(self, driver):
        """
        Prueba que un usuario no autenticado no pueda acceder al formulario de solicitud.
        
        Pasos:
        1. Intenta acceder directamente al formulario de solicitud sin iniciar sesión
        2. Verifica que el sistema redirija a la página de inicio de sesión
        """
        # 1. Intenta acceder directamente a la página de solicitud sin iniciar sesión
        request_page = RequestBloodPage(driver)
        request_page.navigate(REQUEST_BLOOD_URL)
        
        # 2. Verificar redirección a página de login
        login_page = LoginPage(driver)
        
        # Dar tiempo para la redirección
        time.sleep(2)
        
        # La prueba pasa si detecta que estamos en la página de inicio de sesión
        assert login_page.is_login_page() or "/loginUser" in driver.current_url, "Un usuario no autenticado pudo acceder a la página de solicitud de sangre"