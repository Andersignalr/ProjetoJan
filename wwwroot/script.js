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