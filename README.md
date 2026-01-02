# Online Handwriting Quality Analysis Using Deep Learning

## 📌 Overview
This project presents a deep learning–based framework for **online handwriting quality analysis and evaluation**.  
The proposed approach combines **beta-elliptic handwriting modeling** with **convolutional neural networks (CNNs)** to assess handwriting quality using both **dynamic** and **visual** characteristics of the writing trajectory.

The system is designed for **multilingual handwriting** (Arabic, Latin, Persian, Urdu) and targets **educational applications**, particularly for primary school children, by providing automatic feedback on handwriting quality.

This repository accompanies the research paper:
> *Handwriting Quality Analysis Using Deep Learning*  

---

## 🎯 Objectives
- Model online handwriting trajectories using **beta-elliptic representation**
- Analyze handwriting quality based on multiple criteria:
  - Shape accuracy
  - Stroke order
  - Writing direction and dynamics
  - Baseline alignment
- Combine **dynamic features** and **visual features** for robust evaluation
- Provide an **automatic and objective handwriting quality score**
- Support **multilingual handwriting analysis**

---

## 🧠 Methodology

### 1. Handwriting Modeling
- Online handwriting trajectories are decomposed into **beta-elliptic strokes**
- Each stroke captures:
  - Geometric properties
  - Kinematic information (velocity, acceleration)

### 2. Feature Extraction
- **Dynamic features** extracted from beta-elliptic parameters
- **Visual features** obtained from rendered handwriting images

### 3. Deep Learning Architecture
- **CNN-based model** for visual perception
- Fusion of:
  - Beta-elliptic dynamic features
  - CNN visual embeddings
- End-to-end learning for handwriting quality evaluation

### 4. Evaluation Criteria
- Shape correctness
- Stroke sequencing
- Direction consistency
- Baseline compliance

---

## 📱 Application
A mobile application was developed to:
- Capture online handwriting input
- Evaluate handwriting quality in real time
- Provide **pedagogical feedback** to learners

Target users:
- Primary school students
- Teachers and educational institutions

---

## 📊 Dataset
- Online handwriting samples collected from multiple scripts
- Data includes stroke-level temporal and spatial information
- Supports multilingual analysis:
  - Arabic
  - Latin
  - Persian
  - Urdu

---

## 🔬 Experimental Results
- The proposed approach demonstrates:
  - High robustness to writing variability
  - Effective discrimination between good and poor handwriting
- Results validate the relevance of combining **beta-elliptic modeling** with **deep neural networks**

---

## 🛠 Technologies Used
- Python
- Deep Learning (CNNs)
- Signal Processing
- Computer Vision
- Online Handwriting Analysis

---

## 📄 Publication

Hamdi, Y., Akouaydi, H., Boubaker, H. et al. Handwriting quality analysis using online-offline models. Multimed Tools Appl 81, 43411–43439 (2022). https://doi.org/10.1007/s11042-022-13228-w
