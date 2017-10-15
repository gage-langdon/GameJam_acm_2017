import React, { Component } from 'react';
import SocketIO from './socket/index';
import { Holdable, defineHold } from 'react-touch';

import UpArrow from './images/up-arrow.png';
import DownArrow from './images/down-arrow.png';
import LeftArrow from './images/left-arrow.png';
import RightArrow from './images/right-arrow.png';
import aButton from './images/b.png';
import bButton from './images/a.png';

var keyDown = {
	'up': false,
	'down': false,
	'left': false,
	'right': false,
	'fire': false
}

// 3000

const BTN = {
	up: 'up',
	down: 'down',
	left: 'left',
	right: 'right',
	fire: 'fire',
	repair: 'repair'
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
	componentWillMount() {
		document.addEventListener('keydown', (event) => {
			const keyName = event.key;
			switch (keyName) {
				case "w":
					keyDown.up = true;
					break;
				case "d":
					keyDown.right = true;
					break;
				case "s":
					keyDown.down = true;
					break;
				case "a":
					keyDown.left = true;
					break;
				case " ":
					keyDown.fire = true;
					break;
			}
		});
		document.addEventListener('keyup', (event) => {
			const keyName = event.key;
			switch (keyName) {
				case "w":
					keyDown.up = false;
				case "d":
					keyDown.right = false;
					break;
				case "s":
					keyDown.down = false;
					break;
				case "a":
					keyDown.left = false;
					break;
				case " ":
					keyDown.fire = false;
					break;
			}
		});
		this.checkForAction();
	}
	checkForAction() {
		if (keyDown.up)
			this.onControllerInput(BTN.up);
		if (keyDown.left)
			this.onControllerInput(BTN.left);
		if (keyDown.right)
			this.onControllerInput(BTN.right);
		if (keyDown.down)
			this.onControllerInput(BTN.down);
		if (keyDown.fire)
			this.onControllerInput(BTN.fire)
		setTimeout(() => {
			this.checkForAction();
		}, 1);
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
		let dPadImgStyleU = {
			width: '100%',
			height: '100%',
			bottom: 0
		}
		let dPadImgStyleR = {
			width: '100%',
			height: '100%',
			left: 0
		}
		let dPadImgStyleL = {
			width: '100%',
			height: '100%',
			right: 0
		}
		let dPadImgStyleD = {
			width: '100%',
			height: '100%',
			top: 0
		}
		let fireButton = () => (
			<div
				style={{
					height: '100%',
					width: '100%',
					backgroundColor: '#FFFFFF',
					border: 'solid red 1px'
				}}
				onClick={() => this.onControllerInput(BTN.fire)}>
				fire
				</div>
		);

		if (false && this.state.isJoinedGame)
			return (
				<div className="container-fluid">
					<div className="row">
						<div className="col-6" style={{ position: 'absolute', height: '75%', bottom: 0 }}>
							<div className="row h-100">
								{/* top */}
								<div className="col-4 p-0 m-0" />
								<div className="col-4 p-0" >
									<Holdable
										config={holdConfig}
										onHoldProgress={() => this.onControllerInput(BTN.up)}
									>
										Up
									</Holdable>
								</div>
								<div className="col-4 p-0 m-0" />
								{/* middle */}
								<div className="col-4 p-0 m-0">
									<Holdable
										config={holdConfig}
										onHoldProgress={() => this.onControllerInput(BTN.left)}
									>
										left
									</Holdable>
								</div>
								<div className="col-4 p-0 m-0" />
								<div className="col-4 p-0 m-0">
									<Holdable
										config={holdConfig}
										onHoldProgress={() => this.onControllerInput(BTN.right)}
									>
										Right
									</Holdable>
								</div>
								{/* bottom */}
								<div className="col-4 p-0 m-0" />
								<div className="col-4 p-0 m-0" >
									<Holdable
										config={holdConfig}
										onHoldProgress={() => this.onControllerInput(BTN.down)}
									>
										Down
									</Holdable>
								</div>
								<div className="col-4 p-0 m-0" />
							</div>
						</div>
						<div className="col-6" style={{ position: 'absolute', height: '100%', right: 0 }}>
							<div className="row h-100">
								{/* top */}
								<div className="col-4 p-0 m-0" style={{ height: '33.3%' }} />
								<div className="col-4 p-0" style={{ height: '33.3%' }} />
								<div className="col-4 p-0 m-0" style={{ height: '33.3%' }} />
								{/* middle*/}
								<div className="col-4 p-0 m-0" style={{ height: '33.3%' }} />
								<div className="col-4 p-0 m-0" style={{ height: '33.3%' }} />
								<div className="col-4 p-0 m-0" >
									<Holdable
										config={holdConfig}
										onHoldProgress={() => this.onControllerInput(BTN.repair)}
									>
										<img src={bButton} style={dPadImgStyleL} />
									</Holdable>
								</div>
								{/* bottom */}
								<div className="col-4 p-0 m-0" style={{ height: '33.3%' }} />
								<div className="col-4 p-0 m-0" >
									<Holdable
										config={holdConfig}
										onHoldProgress={() => this.onControllerInput(BTN.fire)}
									>
										<img src={aButton} style={dPadImgStyleL} />
									</Holdable>
								</div>
								<div className="col-4 p-0 m-0" style={{ height: '33.3%' }} />
							</div>
						</div>
					</div>
				</div>
			);
		else
			return (
				<div className="container-fluid" >
					<div className="row align-items-center justify-content-center">
						{!this.state.isJoinedGame ?
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
							: <div className="col-6 text-center pt-4">
								<p>WASD - move</p>
								<p>SPACE - shoot</p>
							</div>}
					</div>
				</div>
			)
	}
}

export default App;
