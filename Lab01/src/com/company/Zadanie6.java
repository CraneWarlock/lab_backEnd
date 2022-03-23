package com.company;

import java.util.ArrayList;
import java.io.File;
import java.io.FileNotFoundException;
import java.util.*;

public class Zadanie6 {
    public static void main(String[] args) {
        String text = "text1\ntext2\rtext3\n";

        readFromString(text);
        readFromFile();
    }

    public static void readFromString(String input){
        String[] strings = input.split("\\r?\\n|\\r");

        int i = 1;
        for(String string: strings){
            System.out.println("Line "+i+" - "+string);
            i++;
        }
    }

    public static void readFromFile(){
        List<String> lines = new ArrayList<>();
        try{
            File file = new File("E:\\JavaProjects\\Laby Backend\\lab_backEnd\\Lab01\\src\\com\\company\\Zad6.txt");

            Scanner scanner = new Scanner(file);
            while(scanner.hasNextLine()){
                String line = scanner.nextLine();
                lines.add(line);
            }
            scanner.close();
        } catch (FileNotFoundException e){
            System.out.println("Error");
            e.printStackTrace();
        }

        int i=1;
        for (String string: lines){
            System.out.println("Line "+i+" - "+string);
            i++;
        }
    }

}
