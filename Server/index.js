var app = require('http').createServer()
var io = require('socket.io')(app);
var uuid = require('uuid/v1');

// 8080

var currentPlayers = [];

io.on('connection', (socket) => {
	console.log('player connected ' + socket.id);

	socket.on('OnButtonDown', ({ button }) => {
		let { name, id } = getPlayerByID(socket.id);
		console.log(name, id, button);
		if (id) {
			let event = {
				playerID: id,
				playerName: name,
				button
			}
			console.log(event);
			io.emit("ButtonDown", event);
		}
	});
	socket.on('JoinGame', ({ name }) => {
		addPlayer(socket, name);
	})
});

const getPlayerByName = (playerName) => {
	return currentPlayers.find(({ name }) => playerName === name);
}
const getPlayerByID = (playerID) => {
	return currentPlayers.find(({ id }) => playerID === id) || {};
}
function addPlayer(socket, name) {
	let newPlayer = {
		name,
		id: socket.id
	}
	currentPlayers.push(newPlayer);
	socket.join("room1");
	io.emit('NewPlayer', {
		playerID: newPlayer.id,
		playerName: newPlayer.name
	});
	console.log('new player', newPlayer);
}
app.listen('8080', () => {
	console.log('listening on 8080')
})