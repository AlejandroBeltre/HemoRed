# Configuración básica
BASE_URL = "https://hemo-red.vercel.app/"  # URL del frontend

# Credenciales de usuarios de prueba
USER_CREDENTIALS = {
    "regular": {
        "email": "usuariodeprueba@prueba.com",
        "password": "prueba123",
        "document_number": "40229540931"  # Obtenido de UserContext.js
    },
    "admin": {
        "email": "admin@admin.com",
        "password": "prueba123",
        "document_number": "40229540930"
    }
}

# Datos para solicitud de sangre
BLOOD_REQUEST_DATA = {
    "blood_bank": "test",
    "blood_type": "O-",
    "quantity": "2",
    "reason": "Cirugía de emergencia programada"
}

# Tiempo de espera implícito en segundos
IMPLICIT_WAIT = 10

# URLs específicas
LOGIN_URL = f"{BASE_URL}/loginUser"
REQUEST_BLOOD_URL = f"{BASE_URL}/requestBlood"
REQUEST_STATUS_URL = f"{BASE_URL}/viewBloodRequestStatus"