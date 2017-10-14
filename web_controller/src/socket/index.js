const CONFIG = require('../config');
const SocketIO = require('socket.io-client');
const Socket = SocketIO(CONFIG.socket);

const emit = async (eventname, data) => {
	console.log(eventname, data);
	Socket.emit(eventname, data);
}

export default { emit };