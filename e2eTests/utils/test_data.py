import uuid
import random

def generate_unique_email():
    """Genera un email único para pruebas."""
    return f"test_{uuid.uuid4().hex[:8]}@example.com"

def generate_random_document_number():
    """Genera un número de documento dominicano aleatorio."""
    first_part = random.randint(100, 999)
    second_part = random.randint(1000000, 9999999)
    verification_digit = random.randint(0, 9)
    return f"{first_part}-{second_part}-{verification_digit}"

def setup_test_data(api_base_url):
    """
    Configura datos de prueba iniciales mediante llamadas a la API.
    
    Nota: En un escenario real, deberías implementar esta función para comunicarte
    con el backend y preparar los datos de prueba (crear usuarios, bancos, etc.)
    """
    # Aquí implementarías las llamadas a la API del backend para configurar los datos
    pass

def cleanup_test_data(api_base_url):
    """Limpia los datos de prueba después de las pruebas."""
    # Aquí implementarías las llamadas a la API del backend para limpiar los datos
    pass