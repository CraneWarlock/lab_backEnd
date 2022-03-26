package com.example.demo;

public class UserEntity {

    private String Name;
    private int Age;

    public UserEntity(String name, int age) {
        Name = name;
        Age = age;
    }

    public UserEntity() {
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public int getAge() {
        return Age;
    }

    public void setAge(int age) {
        Age = age;
    }
}
