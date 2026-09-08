document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("loginForm");
    const usuarioInput = document.getElementById("usuario");
    const passwordInput = document.getElementById("password");
    const usuarioError = document.getElementById("usuarioError");
    const passwordError = document.getElementById("passwordError");
    const formError = document.getElementById("formError");
    const btnIngresar = document.getElementById("btnIngresar");

    // Ruta de tu action de MVC, ej: [HttpPost] public async Task<ActionResult> Login(LoginViewModel modelo)
    // dentro de AccountController. Ajusta el controlador/acción si usas otro nombre.
    const LOGIN_ENDPOINT = "/Account/Login";

    function limpiarErrores() {
        usuarioError.textContent = "";
        passwordError.textContent = "";
        formError.textContent = "";
        usuarioInput.classList.remove("has-error");
        passwordInput.classList.remove("has-error");
    }

    function validar() {
        let esValido = true;

        if (!usuarioInput.value.trim()) {
            usuarioError.textContent = "Ingresa tu usuario o correo.";
            usuarioInput.classList.add("has-error");
            esValido = false;
        }

        if (!passwordInput.value) {
            passwordError.textContent = "Ingresa tu contraseña.";
            passwordInput.classList.add("has-error");
            esValido = false;
        }

        return esValido;
    }

    function setCargando(cargando) {
        btnIngresar.disabled = cargando;
        btnIngresar.querySelector(".btn-text").textContent = cargando
            ? "Ingresando..."
            : "Ingresar";
    }

    form.addEventListener("submit", async (event) => {
        event.preventDefault();
        limpiarErrores();

        if (!validar()) {
            return;
        }

        // MVC: el token viene del hidden input que genera @Html.AntiForgeryToken()
        const token = document.querySelector(
            'input[name="__RequestVerificationToken"]'
        )?.value;

        const datos = {
            usuario: usuarioInput.value.trim(),
            password: passwordInput.value,
        };

        setCargando(true);

        try {
            const respuesta = await fetch(LOGIN_ENDPOINT, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    // MVC valida este header junto con [ValidateAntiForgeryToken]
                    // en el action, o con un ValidateAntiForgeryTokenAttribute
                    // personalizado para peticiones AJAX/JSON.
                    RequestVerificationToken: token || "",
                },
                body: JSON.stringify(datos),
            });

            if (!respuesta.ok) {
                const error = await respuesta.json().catch(() => null);
                formError.textContent =
                    (error && error.mensaje) || "Usuario o contraseña incorrectos.";
                return;
            }

            const resultado = await respuesta.json();

            // MVC clásico normalmente maneja la sesión con cookie de autenticación
            // (FormsAuthentication / Identity), así que no siempre necesitas guardar
            // un token en el cliente. Si tu action sí devuelve un token, descomenta:
            // if (resultado.token) sessionStorage.setItem("token", resultado.token);

            window.location.href = resultado.redirectUrl || "/Home/Index";
        } catch (err) {
            formError.textContent =
                "No se pudo conectar con el servidor. Intenta de nuevo.";
        } finally {
            setCargando(false);
        }
    });
});