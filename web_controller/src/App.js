import React, { Component } from 'react';
import SocketIO from './socket/index';
import { Holdable, defineHold } from 'react-touch';

// 3000

const BTN = {
	up: 'up',
	down: 'down',
	left: 'left',
	right: 'right',
	fire: 'fire'
}
const holdConfig = defineHold({ updateEvery: 60, holdFor: 9999999999999 });

class App extends Component {
	constructor() {
		super();

		this.onJoinGame = this.onJoinGame.bind(this);

		this.state = {
			name: '',
			isJoinedGame: false
		}
	}
	onControllerInput(button) {
		if (button)
			SocketIO.emit('OnButtonDown', { button });
	}
	async onJoinGame() {
		console.log('join game');
		if (this.state.name) {
			await SocketIO.emit('JoinGame', {
				name: this.state.name
			});
			this.setState({ isJoinedGame: true });
		} else {
			console.log('no name');
		}
	}
	render() {
		let dPadImage = (text) => (
			<div
				style={{
					height: '50px',
					width: '50px',
					backgroundColor: '#FFFFFF',
					border: text ? 'solid black 1px' : ''
				}}>
				{text}
			</div>
		)
		let dPadItem = (buttonDir) => (
			<Holdable
				config={holdConfig}
				onHoldProgress={() => this.onControllerInput(buttonDir)}
			>
				{dPadImage(buttonDir)}
			</Holdable>
		);
		let fireButton = () => (
			<div
				style={{
					height: '50px',
					width: '50px',
					backgroundColor: '#FFFFFF',
					border: 'solid red 1px'
				}}
				onClick={() => this.onControllerInput(BTN.fire)}>
				fire
				</div>
		);

		if (this.state.isJoinedGame)
			return (
				<div className="container-fluid">
					<div className="row">
						<div className="px-0">
							{dPadItem()}
							{dPadItem(BTN.left)}
							{dPadItem()}
						</div>
						<div className="px-0">
							{dPadItem(BTN.up)}
							{dPadItem()}
							{dPadItem(BTN.down)}
						</div>
						<div className="px-0">
							{dPadItem()}
							{dPadItem(BTN.right)}
							{dPadItem()}
						</div>
						<div className="pl-5">
							{dPadItem()}
							{dPadItem()}
							{fireButton()}
						</div>
					</div>
				</div>
			);
		else
			return (
				<div className="container-fluid">
					<div className="row align-items-center justify-content-center">
						<div className="col-6 text-center pt-4">
							<input
								type="text"
								className="form-control"
								placeholder="Username"
								value={this.state.name}
								onChange={({ target }) => this.setState({ name: target.value })} />
							<button
								className="btn btn-primary"
								onClick={() => this.onJoinGame()}>
								Join Game
							</button>
						</div>
					</div>
				</div>
			)
	}
}

export default App;
