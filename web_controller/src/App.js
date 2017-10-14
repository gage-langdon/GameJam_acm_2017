import React, { Component } from 'react';
import SocketIO from './socket/index';
import { Holdable, defineHold } from 'react-touch';

import UpArrow from './images/up-arrow.png';
import DownArrow from './images/down-arrow.png';
import LeftArrow from './images/left-arrow.png';
import RightArrow from './images/right-arrow.png';
import aButton from './images/b.png';
import bButton from './images/a.png';

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
			name: 'gage',
			isJoinedGame: false
		}
	}
	onControllerInput(button) {
		if (button)
			SocketIO.emit('OnButtonDown', { button });
	}
	componentWillMount() {
		this.onJoinGame();
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

		if (true || this.state.isJoinedGame)
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
										<img className="img-responsive" src={UpArrow} style={dPadImgStyleU} />
									</Holdable>
								</div>
								<div className="col-4 p-0 m-0" />
								{/* middle */}
								<div className="col-4 p-0 m-0">
									<Holdable
										config={holdConfig}
										onHoldProgress={() => this.onControllerInput(BTN.left)}
									>
										<img src={LeftArrow} style={dPadImgStyleL} />
									</Holdable>
								</div>
								<div className="col-4 p-0 m-0" />
								<div className="col-4 p-0 m-0">
									<Holdable
										config={holdConfig}
										onHoldProgress={() => this.onControllerInput(BTN.right)}
									>
										<img src={RightArrow} style={dPadImgStyleR} />
									</Holdable>
								</div>
								{/* bottom */}
								<div className="col-4 p-0 m-0" />
								<div className="col-4 p-0 m-0" >
									<Holdable
										config={holdConfig}
										onHoldProgress={() => this.onControllerInput(BTN.down)}
									>
										<img src={DownArrow} style={dPadImgStyleD} />
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
