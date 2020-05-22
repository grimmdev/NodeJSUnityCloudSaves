'use strict';
var express = require('express');
var router = express.Router();

var low = require('lowdb');
var FileSync = require('lowdb/adapters/FileSync');

var adapter = new FileSync('db.json');
var db = low(adapter);

db.defaults({ saves: [] }).write();

router.get('/count', function (req, res) {
	let count = db.get('saves').size().value();

	if (count == null) {
		res.json({});
	} else {
		res.json({ count: count });
	}
});

router.get('/all', function (req, res) {
	let records = db.get('saves').value();

	if (records == null) {
		res.json({});
	} else {
		res.json(records);
	}
});

router.post('/save', function (req, res) {
	let guid = req.body.guid;
	let data = req.body.data;

	let previousSave = db.get('saves').find({ id: guid }).value();

	if (previousSave == null) {
		db.get('saves').push({ id: guid, data: data }).write();
	} else {
		db.get('saves').find({ id: guid }).assign({ data: data }).write();
	}
	res.json({});
});

router.get('/load/:guid', function (req, res) {
	let guid = req.params.guid;

	let previousSave = db.get('saves').find({ id: guid }).value();

	if (previousSave == null) {
		res.json({});
	} else {
		res.json(JSON.parse(previousSave.data));
	}
});

module.exports = router;