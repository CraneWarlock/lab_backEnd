package com.company;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;

public class Main {

    public static void main(String[] args) {
        File file = new File("E:\\JavaProjects\\lab_backEnd\\src\\file.txt");
        try (
                FileInputStream inputStream = new FileInputStream(file);
                InputStreamReader fileReader = new InputStreamReader(inputStream, "UTF-8");
        )
        {
            char[] buffer = new char[10];

            while(true){
                int readBytes = fileReader.read(buffer,0, buffer.length);
                if (readBytes == -1){
                    break;
                }
                System.out.println(buffer);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}