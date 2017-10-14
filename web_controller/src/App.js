import React, { Component } from 'react';
import SocketIO from './socket/index';
import { Holdable, defineHold } from 'react-touch';

// 3000

const BTN = {
	up: 'up',
	down: 'down',
	left: 'left',
	right: 'right'
}
const holdConfig = defineHold({ updateEvery: 60, holdFor: 9999999999999 });

class App extends Component {
	onControllerInput(button) {
		if (button)
			SocketIO.emit('OnButtonDown', { button });
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
				</div>
			</div>
		);
	}
}

export default App;
