document.getElementById("addUserForm").addEventListener("submit", function (event) {
    event.preventDefault(); // Varsayilan form g�nderimini engelle

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const role = document.getElementById("role").value;

    // Yeni kullanici olusturma istegi g�nderme
    fetch('/api/Admin/create-user', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Username: username, Password: password, Role: role })
    })
        .then(response => {
            if (response.ok) {
                alert("Kullanici basariyla eklendi.");
                // Kullan?c? listesini g�ncelle
                loadUsers();
            } else {
                throw new Error('Kullanici eklenemedi!');
            }
        })
        .catch(error => {
            alert(error.message);
        });
});

// Kullanicilari y�kle
function loadUsers() {
    fetch('/api/Admin/get-users') // Kullanicilari alacak endpoint
        .then(response => response.json())
        .then(users => {
            const tbody = document.querySelector("#userTable tbody");
            tbody.innerHTML = ""; // �nceki verileri temizle

            users.forEach(user => {
                const row = document.createElement("tr");
                row.innerHTML = `
                    <td>${user.Username}</td>
                    <td>${user.Role}</td>
                    <td>
                        <button onclick="deleteUser('${user.Id}')">Sil</button>
                    </td>
                `;
                tbody.appendChild(row);
            });
        });
}

// Kullanici silme fonksiyonu
function deleteUser(userId) {
    fetch(`/api/Admin/delete-user/${userId}`, { method: 'DELETE' })
        .then(response => {
            if (response.ok) {
                alert("Kullanici basariyla silindi.");
                loadUsers(); // Kullan?c? listesini g�ncelle
            } else {
                throw new Error('Kullanici silinemedi!');
            }
        })
        .catch(error => {
            alert(error.message);
        });
}

// Sayfa y�klendiginde kullanicilari y�kle
window.onload = loadUsers;
