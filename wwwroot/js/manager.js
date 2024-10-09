window.onload = function () {
    const managerId = /* Yönetici ID'sini buradan al */;

    fetch(`/api/Log/user-logs/${managerId}`)
        .then(response => response.json())
        .then(logs => {
            const tbody = document.querySelector("#logTable tbody");
            tbody.innerHTML = ""; // Önceki verileri temizle

            logs.forEach(log => {
                const row = document.createElement("tr");
                row.innerHTML = `
                    <td>${log.UserId}</td>
                    <td>${new Date(log.LoginTime).toLocaleString()}</td>
                    <td>${log.LogoutTime ? new Date(log.LogoutTime).toLocaleString() : 'Henüz çıkış yapılmadı'}</td>
                `;
                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Log yüklenirken hata oluştu: ", error);
        });
};
