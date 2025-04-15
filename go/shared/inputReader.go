package shared

import (
	"io"
	"os"
)

var NewLine = "\r\n"

func ReadProblemInput(problem string) (string, error) {
	file, err := os.Open("../../input/" + problem + ".txt")
	if err != nil {
		return "", err
	}
	defer file.Close()

	content, err := io.ReadAll(file)
	if err != nil {
		return "", err
	}
	return string(content), nil
}
