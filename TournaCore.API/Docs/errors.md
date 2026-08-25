## Error responses

All application errors return the following format:

```json
{
	"errorCode": 1101,
	"errorMessage": "Example error message"
}
```

## Error codes

| Code	 | Description |
|--------|-------------|
| `1001` | Validation failed |
| `1101` | Email already exists |
| `1102` | Invalid credentials |

## Validation errors

Validation errors use error code `1001` and return the following format:

```json
{
	"errorCode": 1001,
	"errorMessage": "Validation failed",
	"errors": {
		"email": [
			"The Email field is required."
		]
	}
}
```