document.getElementById("loginForm").addEventListener("submit", function (event) {
    event.preventDefault(); // Varsayılan form gönderimini engelle

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    // Giriş yapma isteği gönderme
    fetch('/Login/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: new URLSearchParams({
            'username': username,
            'password': password
        })
    })
        .then(response => {
            if (response.ok) {
                return response.text(); // Başarılı yanıt alındı
            } else {
                throw new Error('Giriş başarısız!'); // Hata durumu
            }
        })
        .then(data => {
            alert("Giriş başarılı!"); // Başarılı giriş
            // Burada yönlendirme yapabilirsiniz
            window.location.href = '/Home/Index'; // Ana sayfaya yönlendirme
        })
        .catch(error => {
            alert(error.message); // Hata mesajı göster
        });
});
