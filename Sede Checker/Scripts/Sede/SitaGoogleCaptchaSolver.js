return SolveGoogleCaptcha(arguments[0][0]);

function SolveGoogleCaptcha(googleCaptchaKey) {
    document.getElementById('g-recaptcha-response').value = googleCaptchaKey;
    document.getElementById('btnEnviar').disabled = false;
}