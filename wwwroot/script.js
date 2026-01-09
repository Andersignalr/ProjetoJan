const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

connection.on("ReceiveMessage", (user, message) => {
    console.log(`mensagem recebida: ${user} ${message}`);
});


connection.start()
    .catch(err => console.error(err.toString()));


function enviar() {
    const input = document.getElementById("texto");
    const mensagem = input.value;

    if (!mensagem) return;

    connection.invoke("SendMessage", "Usuário", mensagem)
        .catch(err => console.error(err.toString()));

    input.value = "";
}

async function login() {
    const res = await fetch("/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            email: email.value,
            password: password.value
        })
    });

    if (res.ok) {
        window.location.href = "/index.html";
    } else {
        document.getElementById("erro").innerText = "Usuário ou senha inválidos";
    }
}

async function register() {
    const response = await fetch("/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            email: email.vale,
            password: password.value,
            username: username.value
        })
    });
    if (response.ok) {
        window.location.href = "/index.html";
    } else {
        const erro = await response.json();
        document.getElementById("erro").innerText =
            erro[0]?.description ?? "Erro ao registrar usuário";
    }
}