import pytest
import os
import datetime

def run_tests():
    """Ejecutar pruebas y generar reporte."""
    # Crear directorio para reportes si no existe
    report_dir = "reports"
    if not os.path.exists(report_dir):
        os.makedirs(report_dir)
    
    # Nombre del archivo de reporte con timestamp
    timestamp = datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
    report_file = f"{report_dir}/blood_request_flow_test_{timestamp}.html"
    
    # Ejecutar pruebas con pytest y generar reporte HTML
    pytest.main([
        "test_cases/test_blood_request_flow.py",
        "-v",
        f"--html={report_file}",
        "--self-contained-html"
    ])
    
    print(f"Reporte generado: {report_file}")

if __name__ == "__main__":
    run_tests()