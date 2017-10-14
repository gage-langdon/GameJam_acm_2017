var app = require('http').createServer(onHttpRequest)
var io = require('socket.io')(app);
var uuid = require('uuid/v1');

// 8080

var currentPlayers = [];

io.on('connection', (socket) => {
	console.log('player connected ' + socket.id);
	onPlayerJoin(socket);

	socket.on('OnButtonDown', (data) => {
		console.log('button down', data);
		io.emit("ButtonDown", data);
	});
});
function onHttpRequest(req, res) {
	res.end('hi');
}
function onPlayerJoin(socket) {
	let player = {
		id: socket.id
	}
	currentPlayers.push(player);
	socket.join("room1");
	socket.broadcast.emit("player-join", {
		playerID: player.id
	});
	socket.emit('joined-game', {
		playerID: player.id
	});

	// populate players for new client
	currentPlayers
		.filter(x => x.id != socket.id)
		.forEach((player) => {
			socket.emit('player-join', {
				playerID: player.id
			});
		});
}
app.listen('8080', () => {
	console.log('listening on 8080')
})